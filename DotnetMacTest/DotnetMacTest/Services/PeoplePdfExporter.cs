using DocumentFormat.OpenXml.Spreadsheet;
using DontetMacTest.Shared.DTOs;
using DotnetMacTest.Data;
using DotnetMacTest.Services.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Colors = QuestPDF.Helpers.Colors;

namespace DotnetMacTest.Services;

public class PeoplePdfExporter : IPeoplePdfExporter
{
    public string ContentType => "application/pdf";
    
    public ExportResultDto ExportPeopleToPdfAsync(IEnumerable<Person> people)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        byte[] content = CreateDocument(people);
        return new ExportResultDto("Popis_osoba.pdf", content, this.ContentType);
    }
    
    private static byte[] CreateDocument(IEnumerable<Person> people)
    {
        var document = Document.Create(container =>
            {

                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Lato"));

                    page.Header()
                        .Row(row =>
                        {
                            row.AutoItem()
                                .Column(column =>
                                {
                                    column.Item()
                                        .Text("Ime neke firme");

                                    column.Item()
                                        .Text("Adresa te firme");
                                });
                        });
                    
                    page.Content()
                        .Column(x =>
                        {
                            x.Spacing(20);
                        
                            x.Item().AlignCenter().Text("Tablica s popisom ljudi").FontSize(20).Bold();

                            x.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                table.Cell().Row(1).Column(1).Element(BlockColor).Text("Ime");
                                table.Cell().Row(1).Column(2).Element(BlockColor).Text("Prezime");

                                for (int i = 0; i < people.Count(); i++)
                                {
                                    var person = people.ElementAt(i);
                                    table.Cell().Row((uint)i + 2).Column(1).Element(Block).Text(person.FirstName);
                                    table.Cell().Row((uint)i + 2).Column(2).Element(Block).Text(person.LastName);
                                }
                            });
                        });

                    page.Footer()
                        .AlignRight()
                        .Text(x => { x.CurrentPageNumber(); });
                    
                    // for simplicity, you can also use extension method described in the "Extending DSL" section
                    static IContainer Block(IContainer container)
                    {
                        return container
                            .Border(1)
                            .ShowOnce()
                            .AlignCenter()
                            .AlignMiddle();
                    }
                    
                    static IContainer BlockColor(IContainer container)
                    {
                        return container
                            .Border(1)
                            .Background(Colors.Grey.Lighten3)
                            .ShowOnce()
                            .AlignCenter()
                            .AlignMiddle();
                    }
                });
            })
            .GeneratePdf();

        return document;
    }
}