namespace WFAdmissionDocuments.Code.Pdf.Interfaces
{
    public interface ISettingsData
    {
        string VisibleName { get; set; }
        string ExcelFileSaveLocation { get; set; }
        bool SaveToExcel { get; set; }
        bool PrePrintedPage { get; set; }
        bool DisplayBorders { get; set; }
        bool EmbedFonts { get; set; }
    }
}
