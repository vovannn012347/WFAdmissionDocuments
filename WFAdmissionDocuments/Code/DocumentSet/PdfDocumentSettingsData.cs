using WFAdmissionDocuments.Code.Pdf.Interfaces;

namespace WFAdmissionDocuments.DocumentSet.Code
{
    public class PdfDocumentSetSettingsData : ISettingsData
    {
        public string VisibleName { get; set; }
        public string ExcelFileSaveLocation { get; set; }
        public bool SaveToExcel { get; set; }
        public bool PrePrintedPage { get; set; }
        public bool DisplayBorders { get; set; }
        public bool EmbedFonts { get; set; }
    }   
}
