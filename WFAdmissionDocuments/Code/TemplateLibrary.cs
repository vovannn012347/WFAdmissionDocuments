namespace WFAdmissionDocuments.Code
{
    public class TemplateLibrary
    {
        public static PdfTemplatePropertiesAbriged CreateNewTemplate(string name)
        {
            var ret = new PdfTemplatePropertiesAbriged
            {
                Name = name,
                TemplateWithElements = new PdfTemplateProperties()
            };

            ret.TemplateWithElements.GetDataFromAbriged(ret);

            return ret;
        }
    }
}
