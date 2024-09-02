using System.Collections.Generic;
using System.Drawing;
using iText.Kernel.Font;
using iText.Layout;

namespace WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces
{
    public interface ITemplateKeyedText : ITemplateText
    {
        string TemplateKey { get; set; }
        string FriendlyKeyName { get; set; }
    }
}
