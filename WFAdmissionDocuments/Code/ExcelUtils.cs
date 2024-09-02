using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WFAdmissionDocuments.Code
{
    public static class ExcelUtils
    {
        public static string GetRelativePath(string filePath)
        {
            // Ensure that both the file path and the current directory are full paths
            string fullFilePath = Path.GetFullPath(filePath);
            string currentDirectory = Directory.GetCurrentDirectory();

            Uri fileUri = new Uri(fullFilePath);
            Uri currentDirectoryUri = new Uri(currentDirectory + Path.DirectorySeparatorChar);  // Ensure it's a directory URI

            Uri relativeUri = currentDirectoryUri.MakeRelativeUri(fileUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            // Convert URI path separators to system path separators
            return relativePath.Replace('/', Path.DirectorySeparatorChar);
        }

        public static void WriteExcelLine(string excelFileSaveLocation, Dictionary<string, string> sourceValues, List<string> outputKeysList = null)
        {
            if (string.IsNullOrEmpty(excelFileSaveLocation)) return;

            if (!Directory.Exists(Path.GetDirectoryName(excelFileSaveLocation))) Directory.CreateDirectory(Path.GetDirectoryName(excelFileSaveLocation));

            SpreadsheetDocument spreadsheetDocument;
            if (!File.Exists(excelFileSaveLocation))
            {
                spreadsheetDocument = SpreadsheetDocument.Create(excelFileSaveLocation, SpreadsheetDocumentType.Workbook);
            }
            else
            {
                spreadsheetDocument = SpreadsheetDocument.Open(excelFileSaveLocation, true);
            }


            using (spreadsheetDocument)
            {
                WorkbookPart workbookPart;
                if (spreadsheetDocument.WorkbookPart == null)
                {
                    workbookPart = spreadsheetDocument.AddWorkbookPart();
                }
                else
                {
                    workbookPart = spreadsheetDocument.WorkbookPart;
                }

                if (workbookPart.Workbook == null)
                {
                    workbookPart.Workbook = new Workbook();
                }

                WorksheetPart worksheetPart;
                if (!workbookPart.WorksheetParts.Any())
                {
                    worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                }
                else
                {
                    worksheetPart = workbookPart.WorksheetParts.FirstOrDefault();
                }

                if (worksheetPart.Worksheet == null)
                    worksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets;
                if (!(spreadsheetDocument.WorkbookPart.Workbook.Any() && spreadsheetDocument.WorkbookPart.Workbook.Sheets != null))
                {
                    sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());
                }
                else
                {
                    sheets = spreadsheetDocument.WorkbookPart.Workbook.Sheets;
                }


                Sheet sheet = workbookPart.Workbook.Sheets.Elements<Sheet>().FirstOrDefault();

                SheetData sheetData;

                if (sheet == null)
                {
                    sheet = new Sheet()
                    {
                        Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = "Saved data"
                    };
                    sheets.Append(sheet);

                    sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                    WriteHeaders(sheetData, (outputKeysList != null && outputKeysList.Count > 0) ? outputKeysList : new List<string>(sourceValues.Keys));
                }
                else
                {
                    sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                    if (!TestHeadersAvailable(sheetData, sourceValues))
                    {
                        //remove sheet with header data
                        worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                        workbookPart.DeletePart(worksheetPart);

                        worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                        worksheetPart.Worksheet = new Worksheet(new SheetData());

                        sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
                        WriteHeaders(sheetData, (outputKeysList != null && outputKeysList.Count > 0) ? outputKeysList : new List<string>(sourceValues.Keys));
                    }
                }

                WriteNewRowData(sheetData, sourceValues, outputKeysList);
                

                //// Append a new worksheet and associate it with the workbook


                //// Get the SheetData object
                //SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                //// Create a row and add it to the sheet
                //Row row = new Row();
                //sheetData.Append(row);

                //// Create cells and add them to the row
                //row.Append(
                //    CreateCell("ID"),
                //    CreateCell("Name"),
                //    CreateCell("Age")
                //);

                //// Create another row and add data
                //Row dataRow = new Row();
                //sheetData.Append(dataRow);

                //dataRow.Append(
                //    CreateCell("1"),
                //    CreateCell("John Doe"),
                //    CreateCell("30")
                //);

                //// Create another data row
                //Row dataRow2 = new Row();
                //sheetData.Append(dataRow2);

                //dataRow2.Append(
                //    CreateCell("2"),
                //    CreateCell("Jane Smith"),
                //    CreateCell("25")
                //);

                // Save the worksheet
                worksheetPart.Worksheet.Save();
            }
        }

        private static bool WriteNewRowData(SheetData sheetData, Dictionary<string, string> outputKeys, List<string> outputKeysList = null)
        {
            Row firstRow = sheetData.Elements<Row>().FirstOrDefault();

            Row row = new Row();
            sheetData.Append(row);

            if (firstRow != null)
            {
                if (outputKeysList != null)
                {
                    foreach (Cell cell in firstRow.Elements<Cell>())
                    {
                        var key = cell.InnerText;
                        if (outputKeysList.Contains(key))
                        {
                            row.Append(CreateCell(outputKeys[key]));
                        }
                    }
                }
                else
                {
                    foreach (Cell cell in firstRow.Elements<Cell>())
                    {
                        var key = cell.InnerText;
                        if (outputKeys.ContainsKey(key))
                        {
                            row.Append(CreateCell(outputKeys[key]));
                        }
                    }

                }
                    

                return true;
            }

            return false;
        }

        private static bool TestHeadersAvailable(SheetData sheetData, Dictionary<string, string> outputKeys)
        {
            Row firstRow = sheetData.Elements<Row>().FirstOrDefault();

            if (firstRow != null)
            {
                foreach (Cell cell in firstRow.Elements<Cell>())
                {
                    string cellValue = cell.InnerText;
                    if (!outputKeys.ContainsKey(cellValue)) return false;
                }

                return true;
            }

            return false;
        }
        private static Cell CreateCell(string text)
        {
            Cell cell = new Cell();
            cell.DataType = CellValues.String;
            cell.CellValue = new CellValue(text);
            return cell;
        }

        private static void WriteHeaders(SheetData sheetData, List<string> outputKeysList)
        {
            Row row = new Row();
            sheetData.Append(row);

            foreach (var key in outputKeysList)
            {
                row.Append(CreateCell(key));
            }
        }
    }
}