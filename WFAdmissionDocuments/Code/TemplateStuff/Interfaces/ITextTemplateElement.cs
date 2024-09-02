using System;
using System.Collections.Generic;
using System.Drawing;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;

namespace WFAdmissionDocuments.Code.TemplateStuff
{
    public interface ITextTemplateElement : ITemplateElement
    {
        bool DownScaleText { get; set; }
    }
}
