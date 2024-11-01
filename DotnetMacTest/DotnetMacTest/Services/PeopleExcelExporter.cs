using ClosedXML.Excel;
using ClosedXML.Graphics;
using DontetMacTest.Shared.DTOs;
using DotnetMacTest.Data;
using DotnetMacTest.Services.Interfaces;

namespace DotnetMacTest.Services;

public class PeopleExcelExporter : IPeopleExcelExporter
{
    private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    private const string FileName = "Popis_osoba.xlsx";
    private const int TableStartRow = 1;
    private const int TableStartColumn = 1;

    private const string TableHeaderRecordNumber = "Evidencijski broj";
    private const string TableHeaderFullName = "Ime i prezime";
    private const string TableHeaderOib = "OIB";
    
    private int _tableRowEnd = 0;
    
    public ExportResultDto ExportPeopleToExcelAsync(IEnumerable<Person> people)
    {
        LoadOptions.DefaultGraphicEngine = new DefaultGraphicEngine("DejaVu Sans");
        using var package = new XLWorkbook();
        var worksheet = package.Worksheets.Add("Osobe");
        
        CreateTableHeader(worksheet);
        CreateTableContent(worksheet, people);
        this._tableRowEnd = people.Count() + 1;
        var range = worksheet.Range(TableStartRow, TableStartColumn, this._tableRowEnd, 3);
        range.CreateTable();
        this.AutoFitColumns(worksheet);

        var byteArray = this.GetByteArrayFromExcelPackage(package);
        return new ExportResultDto(FileName, byteArray, ContentType);
    }
    
    private void CreateTableHeader(IXLWorksheet worksheet)
    {
        var currentColumn = TableStartColumn;

        worksheet.Row(TableStartRow).Style.Font.Bold = true;
        worksheet.Row(TableStartRow).Style.Alignment.ShrinkToFit = true;

        worksheet.Cell(TableStartRow, currentColumn++).Value = TableHeaderRecordNumber;
        worksheet.Cell(TableStartRow, currentColumn++).Value = TableHeaderFullName;
        worksheet.Cell(TableStartRow, currentColumn++).Value = TableHeaderOib;
        
        var range = worksheet.Range(TableStartRow, 1, TableStartRow, currentColumn);
        this.SetAllThinBorder(range);
    }
    
    private static void CreateTableContent(IXLWorksheet worksheet, IEnumerable<Person> people)
    {
        var rowNumber = TableStartRow + 1;
    
        var peopleData = people
            .Select(x => new
            {
                x.Id,
                FullName = x.FirstName + " " + x.LastName,
                x.OIB,
            });

        worksheet.Cell(rowNumber, 1).InsertData(peopleData);
    }
    
    private void SetAllThinBorder(IXLRange range)
    {
        range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
    }
    
    private void AutoFitColumns(IXLWorksheet worksheet)
    {
        worksheet.Columns().AdjustToContents();
    }
    
    private byte[] GetByteArrayFromExcelPackage(XLWorkbook package)
    {
        using var memoryStream = new MemoryStream();
        package.SaveAs(memoryStream);
        var byteArray = memoryStream.ToArray();
        memoryStream.Close();
        return byteArray;
    }
}