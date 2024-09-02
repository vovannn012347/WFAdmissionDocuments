using System.Collections.Generic;
using System.Drawing;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;

using WFAdmissionDocuments.Code.Pdf.Interfaces;

namespace WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces
{
    public interface ITemplateData
    {
        float LocationX { get; set; }
        float LocationY { get; set; }
        float SizeW { get; set; }
        float SizeH { get; set; }

        ITemplateElement Parent { get; set; }

        void PopulateFromElement(ITemplateElement element);
        void PopulateToElement(ITemplateElement element);
        ITemplateElement GetRawElement();
        void PopulatePdfCanvas(Canvas canvas, SizeF size, ISettingsData settings, Dictionary<string, iText.Kernel.Font.PdfFont> fontCollection, List<ITemplateData> elementList, Dictionary<string, string> data = null);
        void PrepopulateCanvas(PdfCanvas pdfCanvas, SizeF pageSize, ISettingsData settings, List<ITemplateData> elementList);
    }
}
