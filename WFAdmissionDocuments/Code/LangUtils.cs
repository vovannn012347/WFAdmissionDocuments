using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

using DocumentFormat.OpenXml.Wordprocessing;

using WFAdmissionDocuments.DocumentSets;

namespace WFAdmissionDocuments.Code
{
    public enum SettingsLanguage
    {
        [Description("Language_Ukrainian")]
        Ukrainian,
        [Description("Language_English")]
        English
    }

    public static class LangUtils
    {
        public static CultureInfo GetCulture(SettingsLanguage language)
        {
            switch (language)
            {
                case SettingsLanguage.Ukrainian: return new CultureInfo("uk-UA");
                case SettingsLanguage.English: return new CultureInfo("en-US");
            }

            return new CultureInfo("en");
        }
    }
}
