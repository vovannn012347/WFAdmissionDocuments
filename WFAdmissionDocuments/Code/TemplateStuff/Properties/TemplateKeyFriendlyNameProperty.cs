using System;
using System.Xml;
using WFAdmissionDocuments.Templates.TemplateElements;

namespace WFAdmissionDocuments.Code.TemplateStuff.Properties
{
    public class TemplateKeyFriendlyNameProperty : ElementPropertyProperties
    {
        public override string PropertyName { get => nameof(TextTemplateElement.TemplateKeyName); }
    }
}
