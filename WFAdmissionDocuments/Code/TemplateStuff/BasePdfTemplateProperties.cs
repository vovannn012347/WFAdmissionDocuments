using System.Drawing;

namespace WFAdmissionDocuments.Code
{
    public class BasePdfTemplateProperties
    {
        public string Name { get; set; }
        public SizeF PageSize { get; set; }
        public SettingsData Settings { get; set; } = new SettingsData();
    }
}
