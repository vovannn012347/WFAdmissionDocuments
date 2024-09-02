using System.Collections.Generic;
using System.Drawing;

namespace WFAdmissionDocuments.Code.TemplateStuff
{
    public interface ITemplateKeyElement
    {
        string TemplateKey { get; set; }
        bool UseTextAsFallback { get; set; }
    }
}
