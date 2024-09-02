using iText.IO.Image;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

using WFAdmissionDocuments.Code.Pdf.Interfaces;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;
using WFAdmissionDocuments.Templates.TemplateElements;
using Image = iText.Layout.Element.Image;

namespace WFAdmissionDocuments.Code.TemplateStuff.TemplateData
{
    public class ImageTemplate : ITemplateData, ITemplateImage, IPreprintHideableElement
    {
        [JsonIgnore]
        public ITemplateElement Parent { get; set; }

        public string ImagePath { get; set; }

        public float LocationX { get; set; }
        public float LocationY { get; set; }
        public float SizeW { get; set; }
        public float SizeH { get; set; }
        public bool IsVanishable { get; set; }



        public ITemplateElement GetRawElement()
        {
            return new ImageElement();
        }

        public void PopulateFromElement(ITemplateElement element)
        {
            Parent = element;
            var el = (element as ImageElement);

            this.ImagePath = el.ImagePath;

            this.LocationX = SizeUtils.PixelsToMilimiters(el.Location.X);
            this.LocationY = SizeUtils.PixelsToMilimiters(el.Location.Y);

            this.SizeW = SizeUtils.PixelsToMilimiters(el.Size.Width);
            this.SizeH = SizeUtils.PixelsToMilimiters(el.Size.Height);

            this.IsVanishable = el.IsVanishable;
        }

        public void PopulateToElement(ITemplateElement element)
        {
            var el = (element as ImageElement);

            el.ImagePath = this.ImagePath;
            
            el.Location = new Point(SizeUtils.MillimetersToPixels(this.LocationX), SizeUtils.MillimetersToPixels(this.LocationY));
            el.Size = new Size(SizeUtils.MillimetersToPixels(this.SizeW), SizeUtils.MillimetersToPixels(this.SizeH));
            el.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;

            el.IsVanishable = this.IsVanishable;
        }


        public void PrepopulateCanvas(PdfCanvas pdfCanvas, SizeF size, ISettingsData settings, List<ITemplateData> elementList)
        {

        }

        public void PopulatePdfCanvas(Canvas canvas, SizeF size, ISettingsData settings, Dictionary<string, iText.Kernel.Font.PdfFont> fontCollection, List<ITemplateData> elementList, Dictionary<string, string> data = null)
        {
            var left = SizeUtils.GetMmToPoints(this.LocationX);
            var bottom = SizeUtils.GetMmToPoints(size.Height - this.LocationY - this.SizeH);

            var imageData = ImageDataFactory.Create(ImagePath);
            
            Image image = new Image(imageData, left, bottom);
                image.SetHeight(SizeUtils.GetMmToPoints(SizeH));
                image.SetWidth(SizeUtils.GetMmToPoints(SizeW));
            
            canvas.Add(image);
            canvas.Flush();
        }
    }
}
