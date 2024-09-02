using System.Collections.Generic;
using System.Drawing;
using iText.Kernel.Font;
using iText.Layout;

namespace WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces
{
    public interface ITemplateKeyedMultipartText : ITemplateKeyedText
    {
        string MultipartGroupKey { get; set; }
        int MultipartGroupKeyOrder { get; set; }
    }
}
