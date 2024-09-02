using System;
using System.Windows.Forms;
using System.Xml;
using WFAdmissionDocuments.Code.TemplateStuff.TemplateData.Interfaces;

namespace WFAdmissionDocuments.Code.TemplateStuff.Properties
{
    public class DownScaleTextTextBoxProperty : ElementPropertyProperties
    {
        public override string PropertyName { get => nameof(ITemplateText.DownScaleText); }
    }
}
