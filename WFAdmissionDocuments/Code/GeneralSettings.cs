using System.Drawing;

using WFAdmissionDocuments.DocumentSets;

namespace WFAdmissionDocuments.Code
{
    public class GeneralSettings
    {
        public float? PdfElementBorderWidth { get; set; } = 0.3f;
        public float? DefaultFontSize { get; set; } = 12f;
        public int? RegexHistoryCount { get; set; } = 40;
        public Font DefaultFont { get; set; } = SystemFonts.DefaultFont;
        public SettingsLanguage Language { get; set; } = SettingsLanguage.English;
    }
}
