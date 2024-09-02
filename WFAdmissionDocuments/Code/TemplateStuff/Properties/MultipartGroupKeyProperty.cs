using System;
using System.Xml;
using WFAdmissionDocuments.Templates.TemplateElements;

namespace WFAdmissionDocuments.Code.TemplateStuff.Properties
{
    public class MultipartGroupKeyProperty : ElementPropertyProperties
    {
        public override string PropertyName { get => nameof(TextMultipartTemplateElement.MultipartGroupKey); }
    }
}
