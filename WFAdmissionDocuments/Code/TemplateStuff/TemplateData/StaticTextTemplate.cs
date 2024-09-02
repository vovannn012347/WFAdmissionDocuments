using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Nodes;

using Newtonsoft.Json;

using WFAdmissionDocuments.Code.Pdf.Interfaces;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;
using WFAdmissionDocuments.Templates.TemplateElements;

namespace WFAdmissionDocuments.Code.TemplateStuff.TemplateData
{
    public class StaticTextTemplate : ITemplateText
    {
        [JsonIgnore]
        public ITemplateElement Parent { get; set; }

        public string Text { get; set; }

        public Font Font { get; set; }
        [JsonIgnore]
        public PdfFont OutputFont { get; set; }
        public Color ForeColor { get; set; }
        public bool DownScaleText { get; set; }
        public bool Multiline { get; set; }
        public System.Drawing.ContentAlignment TextAlign { get; set; }

        public float LocationX { get; set; }
        public float LocationY { get; set; }
        public float SizeW { get; set; }
        public float SizeH { get; set; }
        
        public ITemplateElement GetRawElement()
        {
            return new TextStaticElement();
        }

        public void PopulateFromElement(ITemplateElement element)
        {
            Parent = element;
            var el = (element as TextStaticElement);

            this.Text = el.Text;
            this.DownScaleText = el.DownScaleText;

            this.Font = el.Font;
            if (el.Font == null || el.Font == Constants.DefaultFont) this.Font = Constants.DefaultFont;

            this.ForeColor = el.ForeColor;
            this.Multiline = el.Multiline;
            this.TextAlign = el.TextAlign;

            this.LocationX = SizeUtils.PixelsToMilimiters(el.Location.X);
            this.LocationY = SizeUtils.PixelsToMilimiters(el.Location.Y);

            this.SizeW = SizeUtils.PixelsToMilimiters(el.Size.Width);
            this.SizeH = SizeUtils.PixelsToMilimiters(el.Size.Height);

        }

        public void PopulateToElement(ITemplateElement element)
        {
            var el = (element as TextStaticElement);
            
            el.Text = this.Text;

            el.Font = this.Font;
            if (this.Font == null) el.Font = Constants.DefaultFont;

            el.ForeColor = this.ForeColor;
            el.Multiline = this.Multiline;
            el.DownScaleText = this.DownScaleText;
            el.TextAlign = this.TextAlign != 0 ? this.TextAlign : ContentAlignment.MiddleLeft;

            el.Location = new Point(SizeUtils.MillimetersToPixels(this.LocationX), SizeUtils.MillimetersToPixels(this.LocationY));
            el.Size = new Size(SizeUtils.MillimetersToPixels(this.SizeW), SizeUtils.MillimetersToPixels(this.SizeH));
        }
        
        public void PrepopulateCanvas(PdfCanvas pdfCanvas, SizeF size, ISettingsData settings, List<ITemplateData> elementList)
        {

        }

        public void PopulatePdfCanvas(Canvas canvas, SizeF size, ISettingsData settings, Dictionary<string, iText.Kernel.Font.PdfFont> fontCollection, List<ITemplateData> elementList, Dictionary<string, string> data = null)
        {

            var text = Text;
            if (!this.Multiline) text = Regex.Replace(text, @"\t|\n|\r", "");

            var left = SizeUtils.GetMmToPoints(this.LocationX);
            var bottom = SizeUtils.GetMmToPoints(size.Height - this.LocationY - this.SizeH);
            var sizew = SizeUtils.GetMmToPoints(this.SizeW);
            var sizeh = SizeUtils.GetMmToPoints(this.SizeH);

            var textHorizontalAlign = TextAlignment.LEFT;

            switch (this.TextAlign)
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

            switch (this.TextAlign)
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
                new Paragraph(Text)
                        .SetTextAlignment(textHorizontalAlign)
                        .SetVerticalAlignment(textVerticalAlign)
                        .SetHeight(sizeh)
                        //.SetMaxWidth(SizeUtils.GetMmToPoints(this.SizeW))
                        //.SetMaxHeight(SizeUtils.GetMmToPoints(this.SizeH))
                        .SetFixedPosition(left, bottom, sizew);

            paragraph.SetProperty(Property.OVERFLOW_X, OverflowPropertyValue.FIT);
            paragraph.SetProperty(Property.OVERFLOW_Y, OverflowPropertyValue.VISIBLE);

            this.OutputFont = PdfUtils.GetFont(fontCollection, this.Font);
            paragraph.SetFont(this.OutputFont);

            Font font = Constants.DefaultFont;
            if (this.Font != null)
            {
                font = this.Font;
            }

            float fontSize = font.SizeInPoints;
            if (font.Italic) paragraph.SetItalic();
            if (font.Bold) paragraph.SetBold();
            if (font.Underline) paragraph.SetUnderline();

            if (DownScaleText)
                fontSize = PdfUtils.FindFontSizeToFitArea(text, font.Name, this.Multiline, fontSize, sizew, sizeh);

            paragraph.SetFontSize(fontSize);

            if (!ForeColor.IsEmpty)
                paragraph.SetFontColor(new iText.Kernel.Colors.DeviceRgb(ForeColor.R, ForeColor.G, ForeColor.B));

            canvas.Add(paragraph);
        }
    }
}
