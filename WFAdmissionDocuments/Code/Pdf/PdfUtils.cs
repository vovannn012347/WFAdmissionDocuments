using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using iText.IO.Font.Otf;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;

using WFAdmissionDocuments.Code.TemplateStuff;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;
using WFAdmissionDocuments.DocumentSet.Code;
using WFAdmissionDocuments.Templates;

namespace WFAdmissionDocuments.Code
{
    public static class PdfUtils
    {
        #region TextScaleProperties
        static PdfDocument pdfForScale;
        static PdfPage pdfPage;

        static int objectsAdded = 0;

        static PdfFont tempDefaultFont = iText.Kernel.Font.PdfFontFactory.CreateFont();
        static Dictionary<string, string> allFontnames = FontHelper.GetAllFontsAndFileNames();
        static Dictionary<string, iText.Kernel.Font.PdfFont> tempFontCollection = new Dictionary<string, iText.Kernel.Font.PdfFont>();


        /*
        I have experienced the same problem myself 
        (and it took me hours to discover what I was doing wrong). 
        As it turns out, you can use a specific PdfFont instance for only one document.
        As soon as you use a PdfFont instance it is linked to that document, 
        and you can no longer use it in another document.
        */

        public static void InitScalePdf()
        {
            pdfForScale = new PdfDocument(new PdfWriter(new System.IO.MemoryStream()));
            pdfPage = pdfForScale.AddNewPage();

            tempDefaultFont = iText.Kernel.Font.PdfFontFactory.CreateFont();
        }
        public static PdfFont GetFont(string fontName)
        {
            if (tempFontCollection.ContainsKey(fontName)) return tempFontCollection[fontName];

            if (allFontnames.ContainsKey(fontName))
            {
                var fontNamePath = allFontnames[fontName];
                tempFontCollection[fontName] = iText.Kernel.Font.PdfFontFactory.CreateFont(fontNamePath);
            }
            else
            {
                tempFontCollection[fontName] = tempDefaultFont;
            }

            return tempFontCollection[fontName];
        }

        public static void DisposeScalePdf()
        {
            if (pdfForScale != null)
            {
                (pdfPage as IDisposable)?.Dispose();
                pdfPage = null;
                (pdfForScale as IDisposable)?.Dispose();
                pdfForScale = null;

                tempDefaultFont = null;
                tempFontCollection.Clear();
            }
        }

        public static float FontAccuracy = 0.2f;

        public class CustomTextRenderer : ParagraphRenderer
        {
            static Type TextRendererType = typeof(TextRenderer);
            static FieldInfo lineProperty = TextRendererType.GetField("line", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

            public int FirstLineTextLength { get; private set; }
            public int VisibleCharacterCount { get; private set; }

            public CustomTextRenderer(Paragraph modelElement) : base(modelElement) { }


            public override IRenderer GetNextRenderer()
            {
                return new CustomTextRenderer((Paragraph)modelElement);
            }

            public override LayoutResult Layout(LayoutContext layoutContext)
            {
                // Perform the layout using the parent class's logic
                LayoutResult result = base.Layout(layoutContext);

                // Calculate the number of visible characters
                VisibleCharacterCount = 0;
                FirstLineTextLength = 0;

                bool firstRenderer = true;

                var area = layoutContext.GetArea().GetBBox();

                // Iterate through the list of child renderers (which are typically LineRenderer instances)
                foreach (var childRenderer in childRenderers)
                {
                    if (childRenderer is TextRenderer textRenderer)
                    {
                        //FieldInfo fieldInfo = TextRendererType.GetField("line", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

                        // Get the value of the field for the given object instance
                        GlyphLine line = (GlyphLine)lineProperty.GetValue(textRenderer);

                        var lineBBox = textRenderer.GetOccupiedArea().GetBBox();

                        if (lineBBox.GetBottom() >= area.GetBottom() && (lineBBox.GetTop() - lineBBox.GetBottom()) <= area.GetTop())
                        {
                            if (firstRenderer)
                            {
                                FirstLineTextLength = line.end;
                                firstRenderer = false;
                            }
                            VisibleCharacterCount = line.end;
                        }
                    }
                }

                return result;
            }
        }

        //breakbywords - if this is on then rounds to nearest word end
        public static int CalculateCharactersThatFit(
            string text, 
            string fontName,
            bool isMultiline,
            float fontSize,
            float availableWidth,
            float availableHeight)
        {
            var font = GetFont(fontName);
            Paragraph textMeasurePatagraph =
                new Paragraph(text);

            textMeasurePatagraph.SetMargin(0);
            textMeasurePatagraph.SetPadding(0);

            textMeasurePatagraph.SetFont(font);
            textMeasurePatagraph.SetWidth(availableWidth);
            textMeasurePatagraph.SetHeight(availableHeight);

            //textMeasurePatagraph.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.FIT);
            textMeasurePatagraph.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);


            if (isMultiline)
            {
                textMeasurePatagraph.SetHeight(availableHeight);
            }
            else
            {
                textMeasurePatagraph.SetHeight(fontSize * 1.1f);
            }

            CustomTextRenderer textRenderer = new CustomTextRenderer(textMeasurePatagraph);
            textMeasurePatagraph.SetNextRenderer(textRenderer);

            Div textSizeDiv = new Div();
            textSizeDiv.SetMargin(0);
            textSizeDiv.SetPadding(0);
            textSizeDiv.SetVerticalAlignment(VerticalAlignment.TOP);
            textSizeDiv.SetFont(font);
            textSizeDiv.SetFontSize(fontSize);

            textSizeDiv.Add(textMeasurePatagraph);

            iText.Kernel.Geom.Rectangle lineTxtRect =
                new iText.Kernel.Geom.Rectangle(0, 0, availableWidth, availableHeight);

            using (Canvas canvas = new Canvas(new PdfCanvas(pdfPage), lineTxtRect))
            {
                textRenderer.SetParent(canvas.GetRenderer());
                canvas.Add(textSizeDiv);

                int ret = 0;

                if (isMultiline)
                {
                    ret = textRenderer.VisibleCharacterCount;
                }
                else
                {
                    ret = textRenderer.FirstLineTextLength;
                }
                canvas.Close();

                return ret;
            }

            return 0;
        }

        public static float FindFontSizeToFitArea(
            string text, 
            string fontName, 
            bool isMultiline,
            float startFontSize,
            float availableWidth, 
            float availableHeight)
        {
            var font = GetFont(fontName);
            Paragraph textSizeMeasurePatagraph =
                new Paragraph(text);

            textSizeMeasurePatagraph.SetMargin(0);
            textSizeMeasurePatagraph.SetPadding(0);

            textSizeMeasurePatagraph.SetFont(font);
            textSizeMeasurePatagraph.SetWidth(availableWidth);

            if (isMultiline)
            {
                textSizeMeasurePatagraph.SetHeight(availableHeight);
            }
            else
            {
                textSizeMeasurePatagraph.SetHeight(startFontSize * 1.1f);
            }

            Div textSizeDiv = new Div();
            textSizeDiv.SetMargin(0);
            textSizeDiv.SetPadding(0);
            textSizeDiv.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            textSizeDiv.SetFont(font);
            
            textSizeDiv.Add(textSizeMeasurePatagraph);

            iText.Kernel.Geom.Rectangle lineTxtRect = 
                new iText.Kernel.Geom.Rectangle(0, 0, availableWidth, availableHeight);

            float fontSizeL = FontAccuracy;
            float fontSizeR = startFontSize;

            using (Canvas canvas = new Canvas(new PdfCanvas(pdfPage), lineTxtRect))
            {
                // Binary search on the font size
                while (Math.Abs(fontSizeL - fontSizeR) > FontAccuracy)
                {
                    float curFontSize = (fontSizeL + fontSizeR) / 2;
                    textSizeDiv.SetFontSize(curFontSize);

                    // It is important to set parent for the current element renderer
                    // to a root renderer.
                    IRenderer renderer =
                        textSizeDiv.CreateRendererSubTree()
                            .SetParent(canvas.GetRenderer());

                    LayoutContext context =
                        new LayoutContext(
                            new LayoutArea(1, lineTxtRect));

                    if (renderer.Layout(context).GetStatus() == LayoutResult.FULL)
                    {
                        fontSizeL = curFontSize;
                    }
                    else
                    {
                        fontSizeR = curFontSize;
                    }
                }
            }

            // Use the biggest font size that is still small enough to fit all the
            // text.
            return fontSizeL;
        }
        #endregion

        #region Key Data properties
        public static Dictionary<string, KeyVariablesNameData> GatherKeyDatas(List<PdfTemplateProperties> TemplatePropertiesList, List<PdfTemplateProperties> PrviousKeyDatas = null)
        {
            Dictionary<string, KeyVariablesNameData> keyData = new Dictionary<string, KeyVariablesNameData>();
            foreach (var prop in TemplatePropertiesList)
            {
                foreach (var element in prop.Elements)
                {
                    if (element is ITemplateKeyedText templateKeyElement && !string.IsNullOrEmpty(templateKeyElement.TemplateKey))
                    {
                        KeyVariablesNameData data;
                        if (keyData.TryGetValue(templateKeyElement.TemplateKey, out data))
                        {
                        }
                        else
                        {
                            data = new KeyVariablesNameData();
                            keyData[templateKeyElement.TemplateKey] = data;
                        }

                        //if there is friendly name it is recorded
                        if (!string.IsNullOrEmpty(templateKeyElement.FriendlyKeyName))
                        {
                            var index = data.FriendlyNames.IndexOf(templateKeyElement.FriendlyKeyName);
                            //if name is already recorded - append source to name sources
                            //otherwise - create new source and put name there
                            //key indexes are recorded
                            if (index > -1)
                            {
                                if (!data.FriendlyNameSources[index].Contains(prop.Name))
                                    data.FriendlyNameSources[index].Add(prop.Name);
                            }
                            else
                            {
                                data.FriendlyNames.Add(templateKeyElement.FriendlyKeyName);
                                data.FriendlyNameSources.Add(new List<string> { prop.Name });
                            }
                        }

                        //record all places where key is used
                        if (!data.KeySources.Contains(prop.Name)) data.KeySources.Add(prop.Name);
                    }
                }
            }

            return keyData;
        }

        private static Dictionary<string, string> GatherKeyValues(List<PdfTemplateProperties> TemplatePropertiesList, 
            Dictionary<string, string> TemplateKeyValues,
            Dictionary<string, KeyVariablesInputProperties> TemplateKeyProperties,
            List<string> keysOrder = null)
        {
            if (keysOrder == null)
                keysOrder = new List<string>();

            var keyDatas = GatherKeyDatas(TemplatePropertiesList);
            List<string> keyNames = keyDatas.Keys.ToList();

            Dictionary<string, string> outputKeys = null;
            if (keyNames.Count > 0)
            {
                using (PdfKeyDataInputForm form = new PdfKeyDataInputForm())
                {
                    form.TemplateKeyDatas = keyDatas;
                    form.TemplateKeys = TemplateKeyValues;
                    form.TemplateKeyProps = TemplateKeyProperties;
                    form.KeysOrdered = keysOrder;

                    //foreach (var keyName in keyNames)
                    //{
                    //    if (TemplateKeyValues.ContainsKey(keyName))
                    //    {
                    //        form.TemplateKeys[keyName] = TemplateKeyValues[keyName];
                    //    }
                    //    else
                    //    {
                    //        form.TemplateKeys[keyName] = string.Empty;
                    //    }
                    //}

                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        outputKeys = form.TemplateKeys;
                    }
                    else return null;

                    //foreach (var keyName in outputKeys.Keys)
                    //{
                    //    TemplateKeyValues[keyName] = outputKeys[keyName];
                    //}
                }
            }

            return outputKeys;
        }

        #endregion
        #region Tools

        public static PdfFont GetFont(
            Dictionary<string, iText.Kernel.Font.PdfFont> fontCollection,
            Font Font)
        {
            if (Font != null && fontCollection.TryGetValue(Font.Name, out var font))
            {
                return font;
            }
            else
            {
                if (!fontCollection.ContainsKey(string.Empty))
                {
                    fontCollection[string.Empty] = iText.Kernel.Font.PdfFontFactory.CreateFont();
                }

                return fontCollection[string.Empty];
            }
        }
        #endregion


        private static void PackImagePath(List<ITemplateData> elements, string rootPath)
        {
            foreach (var element in elements)
            {
                if (element is ITemplateImage el)
                {
                    if (Path.IsPathRooted(el.ImagePath))
                    {
                        if (el.ImagePath.StartsWith(rootPath))
                        {
                            el.ImagePath = el.ImagePath.Remove(0, rootPath.Length).Trim('\\', '/');
                        }
                        /*
                        else
                        {
                            var indexof = el.ImagePath.IndexOf(Constants.TemplatesDirectory);
                            el.ImagePath = el.ImagePath.Remove(0, indexof + Constants.TemplatesDirectory.Length).Trim('\\', '/');
                        }*/
                    }
                }
            }
        }

        private static void UnpackImagePath(List<ITemplateData> elements, string rootPath)
        {
            foreach (var element in elements)
            {
                if (element is ITemplateImage el)
                {
                    if (!Path.IsPathRooted(el.ImagePath)) el.ImagePath = Path.Combine(rootPath, el.ImagePath);
                }
            }
        }

        private static void AddWideParagraph(
            string text, 
            Canvas canvas, 
            Font font,
            Dictionary<string, iText.Kernel.Font.PdfFont> fontCollection,
            float left, 
            float bottom, 
            float width)
        {
            Paragraph paragraph = new Paragraph(text);

            // Set position (e.g., bottom-left corner with a margin)
            paragraph.SetFixedPosition(left, bottom, width);

            if(fontCollection.ContainsKey(font.Name))
            {
                paragraph.SetFont(fontCollection[font.Name]);
            }
            else
            {
                paragraph.SetFont(fontCollection[string.Empty]);
            }

            paragraph.SetFontSize(font.SizeInPoints);

            paragraph.SetFontColor(iText.Kernel.Colors.ColorConstants.BLACK);
            paragraph.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.FIT);
            paragraph.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);

            canvas.Add(paragraph);
        }

        public static void PrintPdf(PdfTemplateProperties TemplateProperties, string filePath, Dictionary<string, string> TemplateKeyDatas)
        {
            var settings = TemplateProperties.Settings;

            var templatesPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);

            if (TemplateProperties.PageSize == SizeF.Empty)
            {
                TemplateProperties.PageSize = new SizeF(210, 297);
            }

            //get required fonts
            List<string> fontNames = new List<string>();
            foreach (var element in TemplateProperties.Elements)
            {
                if (element is ITemplateText textElement)
                {
                    if (!string.IsNullOrEmpty(textElement.Font.Name))
                        fontNames.Add(textElement.Font.Name);
                }
            }
            fontNames = fontNames.Distinct().ToList();

            //create fonts

            Dictionary<string, iText.Kernel.Font.PdfFont> fontCollection = new Dictionary<string, iText.Kernel.Font.PdfFont>();
            
            if (settings.EmbedFonts)
            {
                fontCollection[string.Empty] = iText.Kernel.Font.PdfFontFactory.CreateFont(Constants.DefaultFont.Name, iText.Kernel.Font.PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
            }
            else
            {
                fontCollection[string.Empty] = iText.Kernel.Font.PdfFontFactory.CreateFont(Constants.DefaultFont.Name, iText.Kernel.Font.PdfFontFactory.EmbeddingStrategy.PREFER_NOT_EMBEDDED);
            }

            var installedFonts = FontHelper.GetAllFontsAndFileNames();

            //var fontNamesPath = FontHelper.GetFontPath(fontNames.ToArray());
            for (int i = 0; i < fontNames.Count; ++i)
            {
                if (!string.IsNullOrEmpty(fontNames[i]))
                {
                    var fontNamePath = installedFonts[fontNames[i]];
                    if (installedFonts.ContainsKey(fontNames[i]))
                    {
                        if (settings.EmbedFonts)
                        {
                            fontCollection[fontNames[i]] = iText.Kernel.Font.PdfFontFactory.CreateFont(fontNamePath, iText.Kernel.Font.PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                        }
                        else
                        {
                            fontCollection[fontNames[i]] = iText.Kernel.Font.PdfFontFactory.CreateFont(fontNamePath, iText.Kernel.Font.PdfFontFactory.EmbeddingStrategy.PREFER_NOT_EMBEDDED);
                        }
                    }
                    else
                    {
                        fontCollection[fontNames[i]] = fontCollection[string.Empty];
                    }
                }
            }

            Dictionary<string, string> outputKeys = 
                GatherKeyValues(new List<PdfTemplateProperties> { TemplateProperties }, 
                //modifies values in dictionary
                TemplateKeyDatas,
                new Dictionary<string, KeyVariablesInputProperties>()
                );

            if (outputKeys == null) return;

            if (outputKeys.Keys.Count > 0)
            {
                if (settings.SaveToExcel)
                {
                    ExcelUtils.WriteExcelLine(settings.ExcelFileSaveLocation, outputKeys);
                }
            }


            UnpackImagePath(TemplateProperties.Elements, templatesPath);

            // Create a writer instance
            using (PdfWriter writer = new PdfWriter(filePath))
            {
                // Initialize the document
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    StringBuilder preprintedPages = new StringBuilder();
                    var pagesize = iText.Kernel.Geom.PageSize.A4;
                    Document document = new Document(pdf, pagesize);

                    int page = 1;
                    pdf.AddNewPage();

                    PdfPage pageObj = pdf.GetPage(page);
                    PdfCanvas pdfCanvas = new PdfCanvas(pageObj);
                    Canvas canvas = new Canvas(pdfCanvas, pdf.GetDefaultPageSize());

                    //document.ShowTextAligned(new Paragraph("HEADER"), pagesize.GetLeft(),
                    //        pagesize.GetBottom(), 1, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);

                    //pdfCanvas
                    //    .SetStrokeColor(iText.Kernel.Colors.ColorConstants.BLACK)
                    //    .SetLineWidth(1)
                    //    .Stroke();

                    PdfUtils.InitScalePdf();
                    TemplateProperties.Elements.Reverse();

                    var elementListCopy = new List<ITemplateData>(TemplateProperties.Elements);

                    while (elementListCopy.Any())
                    {
                        var element = elementListCopy.First();

                        element.PrepopulateCanvas(pdfCanvas, TemplateProperties.PageSize, settings, elementListCopy);
                        element.PopulatePdfCanvas(canvas, TemplateProperties.PageSize, settings, fontCollection, elementListCopy, outputKeys);

                        elementListCopy.Remove(element);
                    }

                    canvas.Close();

                    if (settings.PrePrintedPage)
                    {
                        //add another empty page after current page

                        pdf.AddNewPage(pagesize);
                        ++page;
                        preprintedPages.Append(page);

                        PdfPage pageObjP = pdf.GetPage(page);
                        PdfCanvas pdfCanvasP = new PdfCanvas(pageObjP);
                        Canvas canvasP = new Canvas(pdfCanvasP, pagesize);

                        elementListCopy.AddRange(TemplateProperties.Elements);
                        //do not add "vanishable" elements
                        while (elementListCopy.Any())
                        {
                            var element = elementListCopy.First();

                            if (!(element is IPreprintHideableElement preprintHide && preprintHide.IsVanishable))
                            {
                                element.PrepopulateCanvas(pdfCanvasP, TemplateProperties.PageSize, settings, elementListCopy);
                                element.PopulatePdfCanvas(canvasP, TemplateProperties.PageSize, settings, fontCollection, elementListCopy, outputKeys);
                            }
                            elementListCopy.Remove(element);
                        }

                        canvasP.Close();

                        pdf.AddNewPage(pagesize);
                        ++page;
                        preprintedPages.Append(page);

                        pageObjP = pdf.GetPage(page);
                        pdfCanvasP = new PdfCanvas(pageObjP);
                        canvasP = new Canvas(pdfCanvasP, pagesize);

                        AddWideParagraph(
                            StringResources.PrePrintedPagesStr + " " + preprintedPages,
                            canvasP,
                            Constants.DefaultFont,
                            fontCollection,
                            50, pagesize.GetHeight() - 20, pagesize.GetWidth() - 100);

                        canvasP.Close();
                    }

                    TemplateProperties.Elements.Reverse();
                    PdfUtils.DisposeScalePdf();

                    document.Close();
                }
            }

            PackImagePath(TemplateProperties.Elements, templatesPath);
        }

        public static void PrintDocumentSetPdf(PdfDocumentSetProperties docSet, Dictionary<string, PdfTemplatePropertiesAbriged> templatesDict, string filePath)
        {
            var settings = docSet.Settings;

            var templatesPath = Path.Combine(Directory.GetCurrentDirectory(), Constants.TemplatesDirectory);
            List<PdfTemplateProperties> templates = new List<PdfTemplateProperties>();
            
            #region Gather templates

            foreach (var template in docSet.Elements.Keys)
            {
                if(templatesDict.ContainsKey(template))
                    templates.Add(templatesDict[template].TemplateWithElements);
            }

            foreach (var template in templates)
            {
                if (template.PageSize == SizeF.Empty)
                {
                    template.PageSize = Constants.TemplateSizes.A4;
                }
            }
            #endregion

            #region Fonts
            //get required fonts
            List<string> fontNames = new List<string>();
            foreach (var template in templates)
            {
                foreach (var element in template.Elements)
                {
                    if (element is ITemplateText textElement)
                    {
                        if (!string.IsNullOrEmpty(textElement.Font.Name))
                            fontNames.Add(textElement.Font.Name);
                    }
                }
            }
            fontNames = fontNames.Distinct().ToList();

            Dictionary<string, iText.Kernel.Font.PdfFont> fontCollection = new Dictionary<string, iText.Kernel.Font.PdfFont>();

            if (settings.EmbedFonts)
            {
                fontCollection[string.Empty] = iText.Kernel.Font.PdfFontFactory.CreateFont(Constants.DefaultFont.Name, iText.Kernel.Font.PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
            }
            else
            {
                fontCollection[string.Empty] = iText.Kernel.Font.PdfFontFactory.CreateFont(Constants.DefaultFont.Name, iText.Kernel.Font.PdfFontFactory.EmbeddingStrategy.PREFER_NOT_EMBEDDED);
            }

            var installedFonts = FontHelper.GetAllFontsAndFileNames();

            for (int i = 0; i < fontNames.Count; ++i)
            {
                if (!string.IsNullOrEmpty(fontNames[i]))
                {
                    var fontNamePath = installedFonts[fontNames[i]];
                    if (installedFonts.ContainsKey(fontNames[i]))
                    {
                        if (settings.EmbedFonts)
                        {
                            fontCollection[fontNames[i]] = iText.Kernel.Font.PdfFontFactory.CreateFont(fontNamePath, iText.Kernel.Font.PdfFontFactory.EmbeddingStrategy.PREFER_EMBEDDED);
                        }
                        else
                        {
                            fontCollection[fontNames[i]] = iText.Kernel.Font.PdfFontFactory.CreateFont(fontNamePath, iText.Kernel.Font.PdfFontFactory.EmbeddingStrategy.PREFER_NOT_EMBEDDED);
                        }
                    }
                    else
                    {
                        fontCollection[fontNames[i]] = fontCollection[string.Empty];
                    }
                }
            }

            foreach (var template in templates)
            {
                //set fonts for elements
                foreach (var element in template.Elements)
                {
                    if (element is ITemplateText textElement)
                    {
                        if (!string.IsNullOrEmpty(textElement.Font?.Name))
                        {
                            textElement.OutputFont = fontCollection[textElement.Font.Name];
                        }
                        else
                        {
                            textElement.OutputFont = fontCollection[string.Empty];
                        }
                    }
                }
            }

            #endregion


            Dictionary<string, string> outputKeys = 
                GatherKeyValues(templates,
                //do not remember previous values
                docSet.TemplateKeys.ToDictionary(entry => entry.Key, entry => entry.Value),
                docSet.TemplateKeyInputProps,
                docSet.KeysOrder);

            if (outputKeys == null) return;

            if (outputKeys.Keys.Count > 0)
            {
                if (settings.SaveToExcel)
                {
                    ExcelUtils.WriteExcelLine(settings.ExcelFileSaveLocation, outputKeys, docSet.ExcelKeys);
                }
            }
            

            foreach (var template in templates)
            {
                UnpackImagePath(template.Elements, templatesPath);
            }

            // Create a writer instance
            using (PdfWriter writer = new PdfWriter(filePath))
            {
                // Initialize the document
                PdfUtils.InitScalePdf();
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    StringBuilder preprintedPages = new StringBuilder();
                    var pagesize = iText.Kernel.Geom.PageSize.A4;
                    Document document = new Document(pdf, pagesize);

                    PdfPage pageObjP;
                    PdfCanvas pdfCanvasP;
                    Canvas canvasP;

                    bool firstPreprinted = true;
                    int page = 1;
                    foreach (var template in templates)
                    {
                        pdf.AddNewPage(pagesize);
                        PdfPage pageObj = pdf.GetPage(page);
                        PdfCanvas pdfCanvas = new PdfCanvas(pageObj);
                        Canvas canvas = new Canvas(pdfCanvas, pagesize);// pdf.GetDefaultPageSize());

                        template.Elements.Reverse();
                        var elementListCopy = new List<ITemplateData>(template.Elements);

                        while (elementListCopy.Any())
                        {
                            var element = elementListCopy.First();

                            element.PrepopulateCanvas(pdfCanvas, template.PageSize, settings, elementListCopy);
                            element.PopulatePdfCanvas(canvas, template.PageSize, settings, fontCollection, elementListCopy, outputKeys);

                            elementListCopy.Remove(element);
                        }

                        canvas.Close();
                        ++page;

                        if (settings.PrePrintedPage)
                        {
                            //add another empty page after current page

                            pdf.AddNewPage(pagesize);
                            preprintedPages.Append( (firstPreprinted ? "" : ", ") + page);
                            firstPreprinted = false;

                            pageObjP = pdf.GetPage(page);
                            pdfCanvasP = new PdfCanvas(pageObjP);
                            canvasP = new Canvas(pdfCanvasP, pagesize);

                            elementListCopy.AddRange(template.Elements);
                            //do not add "vanishable" elements
                            while (elementListCopy.Any())
                            {
                                var element = elementListCopy.First();

                                if (!(element is IPreprintHideableElement preprintHide && preprintHide.IsVanishable))
                                {
                                    element.PrepopulateCanvas(pdfCanvasP, template.PageSize, settings, elementListCopy);
                                    element.PopulatePdfCanvas(canvasP, template.PageSize, settings, fontCollection, elementListCopy, outputKeys);
                                }
                                elementListCopy.Remove(element);
                            }
                            canvasP.Close();
                            ++page;
                        }

                        template.Elements.Reverse();
                    }


                    if (settings.PrePrintedPage)
                    {
                        pdf.AddNewPage(pagesize);
                        //preprintedPages.Append(page);

                        pageObjP = pdf.GetPage(page);
                        pdfCanvasP = new PdfCanvas(pageObjP);
                        canvasP = new Canvas(pdfCanvasP, pagesize);

                        AddWideParagraph(
                            StringResources.PrePrintedPagesStr + " " + preprintedPages.ToString(),
                            canvasP,
                            Constants.DefaultFont,
                            fontCollection,
                            50, pagesize.GetHeight() - 20, pagesize.GetWidth() - 100);
                        
                        canvasP.Close();
                        ++page;
                    }

                    document.Close();
                }
                PdfUtils.DisposeScalePdf();
            }
            
            foreach (var template in templates)
            {
                PackImagePath(template.Elements, templatesPath);
            }
        }
    }
}