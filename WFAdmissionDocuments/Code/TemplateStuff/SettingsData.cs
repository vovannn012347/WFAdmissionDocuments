using WFAdmissionDocuments.Code.Pdf.Interfaces;

namespace WFAdmissionDocuments.Code
{
    public class SettingsData : ISettingsData
    {
        public string VisibleName { get; set; }
        public string ExcelFileSaveLocation { get; set; }
        public bool SaveToExcel { get; set; } = false;
        public bool PrePrintedPage { get; set; }
        public bool DisplayBorders { get; set; }
        public bool EmbedFonts { get; set; }
    }   
}
