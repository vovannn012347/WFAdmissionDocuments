using System;
using System.Xml;
using WFAdmissionDocuments.Templates.TemplateElements;

namespace WFAdmissionDocuments.Code.TemplateStuff.Properties
{
    public class TemplateKeyProperty : ElementPropertyProperties
    {
        public override string PropertyName { get => nameof(TextTemplateElement.TemplateKey); }
    }
}
