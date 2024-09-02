using System.Drawing;
using System.Globalization;
using System.Resources;

using WFAdmissionDocuments.Code;
using WFAdmissionDocuments.DocumentSets;
using WFAdmissionDocuments.Properties;

namespace WFAdmissionDocuments
{
    static class Constants
    {
        public const string RegexHistoryFile = "RegexHistory.txt";
        public const string GeneralSettingsFile = "GeneralSettings.txt";

        public static string TemplatesDirectory = "Templates";
        public static string ImagesDirectory = "Images";

        public static string DocumentSetsDirectory = "DocumentSets";
        public static string AllTemplatesListNodename = "<<--->>";

        public static string TemplatesExtension = ".tmjson";
        public static string DocumentSetExtension = ".dsjson";

        public const int ResizeHandleSize = 7;
        public static Color SelectionColor => Color.FromArgb(114, 114, 114);
        public static Color HoverColor => Color.FromArgb(80, 80, 80);

        public static int LongClickDuration = 150;



        public static GeneralSettings Settings = null;
        
        public static Font DefaultFont => Settings?.DefaultFont ?? SystemFonts.DefaultFont;
        public static float PdfElementBorderWidth => Settings?.PdfElementBorderWidth ?? 0.3f;
        public static int RegexHistoryCount => Settings?.RegexHistoryCount ?? 40;

        private static SettingsLanguage? Lang = null;
        private static CultureInfo _currentCulture = null;
        public static CultureInfo CurrentCulture
        {
            get
            {
                if(!Lang.HasValue || _currentCulture == null || Settings.Language != Lang)
                {
                    _currentCulture = LangUtils.GetCulture(Settings.Language);
                    Lang = Settings.Language;
                }

                Resources.Culture = _currentCulture;
                return _currentCulture;
            }
        }

        public static class TemplateSizes
        {
            public static SizeF A4 = new SizeF(210, 297);

        }
    }
}
