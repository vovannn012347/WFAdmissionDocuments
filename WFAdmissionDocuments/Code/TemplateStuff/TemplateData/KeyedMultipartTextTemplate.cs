using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

using Newtonsoft.Json;

using WFAdmissionDocuments.Code.Pdf.Interfaces;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;
using WFAdmissionDocuments.Templates.TemplateElements;

namespace WFAdmissionDocuments.Code.TemplateStuff.TemplateData
{
    public class KeyedMultipartTextTemplate : ITemplateKeyedMultipartText
    {
        [JsonIgnore]
        public ITemplateElement Parent { get; set; }

        public string FallbackText { get; set; }
        public string FriendlyKeyName { get; set; }
        public string TemplateKey { get; set; }
        public string MultipartGroupKey { get; set; }
        public int MultipartGroupKeyOrder { get; set; }
        public bool UseTextAsFallback { get; set; }

        public Font Font { get; set; }
        [JsonIgnore]
        public PdfFont OutputFont { get; set; }
        public Color ForeColor { get; set; }
        public bool DownScaleText { get;  set; }
        public bool Multiline { get; set; }
        public System.Drawing.ContentAlignment TextAlign { get; set; }

        public float LocationX { get; set; }
        public float LocationY { get; set; }
        public float SizeW { get; set; }
        public float SizeH { get; set; }
        
        public ITemplateElement GetRawElement()
        {
            return new TextMultipartTemplateElement();
        }

        public void PopulateFromElement(ITemplateElement element)
        {
            Parent = element;
            var el = (element as TextMultipartTemplateElement);

            this.TemplateKey = el.TemplateKey;
            this.MultipartGroupKey = el.MultipartGroupKey;
            this.MultipartGroupKeyOrder = el.MultipartGroupKeyOrder;
            this.FriendlyKeyName = el.TemplateKeyName;
            this.FallbackText = el.Text;
            this.UseTextAsFallback = el.UseTextAsFallback;
            this.DownScaleText = el.DownScaleText;

            this.Font = el.Font;
            if (el.Font == null || el.Font == Constants.DefaultFont) this.Font = Constants.DefaultFont;

            this.ForeColor = el.ForeColor;
            this.TextAlign = el.TextAlign;

            this.Multiline = el.Multiline;

            this.LocationX = SizeUtils.PixelsToMilimiters(el.Location.X);
            this.LocationY = SizeUtils.PixelsToMilimiters(el.Location.Y);

            this.SizeW = SizeUtils.PixelsToMilimiters(el.Size.Width);
            this.SizeH = SizeUtils.PixelsToMilimiters(el.Size.Height);
        }

        public void PopulateToElement(ITemplateElement element)
        {
            var el = (element as TextMultipartTemplateElement);

            el.TemplateKey = this.TemplateKey;
            el.MultipartGroupKey = this.MultipartGroupKey;
            el.MultipartGroupKeyOrder = this.MultipartGroupKeyOrder;
            el.TemplateKeyName = this.FriendlyKeyName;
            el.Text = this.FallbackText;

            el.Font = this.Font;
            if (this.Font == null) el.Font = Constants.DefaultFont;
            el.ForeColor = this.ForeColor;
            el.TextAlign = this.TextAlign != 0 ? this.TextAlign : ContentAlignment.MiddleLeft;

            el.Multiline = this.Multiline;

            el.Location = new Point(SizeUtils.MillimetersToPixels(this.LocationX), SizeUtils.MillimetersToPixels(this.LocationY));
            el.Size = new Size(SizeUtils.MillimetersToPixels(this.SizeW), SizeUtils.MillimetersToPixels(this.SizeH));
        }

        public void PrepopulateCanvas(PdfCanvas pdfCanvas, SizeF size, ISettingsData settings, List<ITemplateData> elementList)
        {
            if (settings.DisplayBorders)
            {
                List<KeyedMultipartTextTemplate> elements = new List<KeyedMultipartTextTemplate>();

                foreach (var someElt in elementList)
                {
                    if (someElt is KeyedMultipartTextTemplate element && element.MultipartGroupKey == this.MultipartGroupKey)
                        elements.Add(element);
                }

                foreach (var element in elements)
                {
                    var left = SizeUtils.GetMmToPoints(element.LocationX) - 3f * Constants.PdfElementBorderWidth;
                    var bottom = SizeUtils.GetMmToPoints(size.Height - element.LocationY - element.SizeH) - 3f * Constants.PdfElementBorderWidth;
                    var sizew = SizeUtils.GetMmToPoints(element.SizeW) + 6 * Constants.PdfElementBorderWidth;
                    var sizeh = SizeUtils.GetMmToPoints(element.SizeH) + 6 * Constants.PdfElementBorderWidth;

                    iText.Kernel.Geom.Rectangle rectangle = new iText.Kernel.Geom.Rectangle(left, bottom, sizew, sizeh);

                    pdfCanvas
                        .SetFillColor(iText.Kernel.Colors.ColorConstants.WHITE)
                        .SetStrokeColor(new iText.Kernel.Colors.DeviceRgb(ForeColor.R, ForeColor.G, ForeColor.B))
                        .SetLineWidth(Constants.PdfElementBorderWidth)
                        .Rectangle(rectangle)
                        .FillStroke();
                }
            }
        }

        public void PopulatePdfCanvas(Canvas canvas, SizeF size, ISettingsData settings, Dictionary<string, iText.Kernel.Font.PdfFont> fontCollection, List<ITemplateData> elementList, Dictionary<string, string> data = null)
        {
            string text = "---";
            if (UseTextAsFallback)
                text = FallbackText;

            if (data.ContainsKey(TemplateKey) && !string.IsNullOrWhiteSpace(data[TemplateKey]))
                text = data[TemplateKey];

            List<KeyedMultipartTextTemplate> elements = new List<KeyedMultipartTextTemplate>();
            
            foreach (var someElt in elementList)
            {
                if (someElt is KeyedMultipartTextTemplate element && element.MultipartGroupKey == this.MultipartGroupKey)
                {
                    elements.Add(element);
                }
            }

            elements = elements.OrderBy(e => e.MultipartGroupKeyOrder).ToList();


            List<string> textParts = new List<string>();
            //gather text part areas for   
            //if word not fully fits - downscale word to fit
            //if still not fits - break word

            float fontSizelMultiplier = 1f;
            bool fits;

            for (float fontSizeL = 0.01f, fontSizeR = 1f, fontSizeCur = fontSizeR; (fontSizeR - fontSizeL) > 0.02; fontSizeCur = (fontSizeL + fontSizeR) / 2)
            {
                fits = false;
                string textTested = (text + string.Empty).Trim();
                textParts.Clear();

                foreach (var element in elements)
                {
                    if (string.IsNullOrWhiteSpace(textTested))
                    {
                        textTested = string.Empty;
                        fits = true;
                        fontSizelMultiplier = fontSizeCur;
                        break;
                    }

                    int count =
                        PdfUtils.CalculateCharactersThatFit(
                            textTested,
                            Font.Name,
                            element.Multiline,
                            element.Font.SizeInPoints * fontSizeCur,
                            SizeUtils.GetMmToPoints(element.SizeW),
                            SizeUtils.GetMmToPoints(element.SizeH));

                    var textPart = textTested.Substring(0, count);

                    textParts.Add(textPart);
                    if (count == textTested.Length)
                    {
                        textTested = string.Empty;
                        fits = true;
                        fontSizelMultiplier = fontSizeCur;
                        break;
                    }
                    else
                    {
                        textTested = textTested.Substring(count).Trim();
                    }
                }

                if(fontSizeCur == fontSizeR && fits)
                {
                    fontSizelMultiplier = fontSizeCur;
                    break;
                }

                if (fits)
                {
                    fontSizeL = fontSizeCur;
                }
                else
                {
                    fontSizeR = fontSizeCur;
                }

                fontSizelMultiplier = fontSizeCur;
            }

            for (int i = 0; i < textParts.Count; ++i)
            {
                var textUsed = textParts[i];
                var element = elements[i];

                if (!element.Multiline) text = Regex.Replace(text, @"\t|\n|\r", "");

                var left = SizeUtils.GetMmToPoints(element.LocationX);
                var bottom = SizeUtils.GetMmToPoints(size.Height - element.LocationY - element.SizeH);
                var sizew = SizeUtils.GetMmToPoints(element.SizeW);
                var sizeh = SizeUtils.GetMmToPoints(element.SizeH);

                var textHorizontalAlign = TextAlignment.LEFT;

                switch (element.TextAlign)
                {
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.BottomLeft:
                        textHorizontalAlign = TextAlignment.LEFT; break;
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.TopRight:
                    case ContentAlignment.BottomRight:
                        textHorizontalAlign = TextAlignment.RIGHT; break;
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.BottomCenter:
                        textHorizontalAlign = TextAlignment.CENTER; break;
                    default:
                        textHorizontalAlign = TextAlignment.LEFT; break;
                }

                var textVerticalAlign = VerticalAlignment.MIDDLE;

                switch (element.TextAlign)
                {
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.MiddleCenter:
                        textVerticalAlign = VerticalAlignment.MIDDLE; break;
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                    case ContentAlignment.TopCenter:
                        textVerticalAlign = VerticalAlignment.TOP; break;
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                    case ContentAlignment.BottomCenter:
                        textVerticalAlign = VerticalAlignment.BOTTOM; break;
                    default:
                        textVerticalAlign = VerticalAlignment.MIDDLE; break;
                }

                Paragraph paragraph =
                    new Paragraph(textUsed)
                            .SetTextAlignment(textHorizontalAlign)
                            .SetVerticalAlignment(textVerticalAlign)
                            .SetHeight(sizeh)
                            .SetFixedPosition(left, bottom, sizew);

                paragraph.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.FIT);
                paragraph.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);

                element.OutputFont = PdfUtils.GetFont(fontCollection, element.Font);
                paragraph.SetFont(element.OutputFont);

                Font font = Constants.DefaultFont;
                if (element.Font != null)
                {
                    font = element.Font;
                }
                float fontSize = font.SizeInPoints;
                if (font.Italic) paragraph.SetItalic();
                if (font.Bold) paragraph.SetBold();
                if (font.Underline) paragraph.SetUnderline();

                fontSize = PdfUtils.FindFontSizeToFitArea(text, font.Name, element.Multiline, fontSize, sizew, sizeh);
                
                paragraph.SetFontSize(fontSize);

                if (!ForeColor.IsEmpty)
                    paragraph.SetFontColor(new iText.Kernel.Colors.DeviceRgb(ForeColor.R, ForeColor.G, ForeColor.B));

                canvas.Add(paragraph);
            }

            foreach (var element in elements)
            {
                elementList.Remove(element);
            }
        }
    }
}
