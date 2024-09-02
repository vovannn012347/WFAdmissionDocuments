using System.Collections.Generic;
using System.Drawing;
using iText.Kernel.Font;
using iText.Layout;

namespace WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces
{
    public interface ITemplateText : ITemplateData
    {
        Font Font { get; set; }
        PdfFont OutputFont { get; set; }
        Color ForeColor { get; set; }
        bool Multiline { get; set; }
        bool DownScaleText { get; set; }
    }
}
