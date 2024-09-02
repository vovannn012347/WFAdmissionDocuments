using System.Collections.Generic;
using System.Drawing;

namespace WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces
{
    public interface ITemplateImage
    {
        ITemplateElement Parent { get; set; }
        string ImagePath { get; set; }
    }
}
