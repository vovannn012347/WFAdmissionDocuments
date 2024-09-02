using System.Collections.Generic;
using System.Drawing;
using iText.Kernel.Font;
using iText.Layout;

namespace WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces
{
    public interface ITemplateKeyedRegexText : ITemplateKeyedText
    {
        string RegexPattern { get; set; }
        int CaptureGroup { get; set; }
    }
}
