using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DontetMacTest.Shared.DTOs;
using DotnetMacTest.Services.Interfaces;
using DotnetMacTest.Data;

namespace DotnetMacTest.Services;

public class WordExporter : IWordExporter
{
    private const string ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
    private const string FileName = "Pozivnice.docx";
    
    public ExportResultDto ExportWord(IEnumerable<Person> people)
    {
        using var mem = new MemoryStream();
        // Create Document
        using (var wordDocument = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document, true))
        {
            // Add a main document part. 
            var mainPart = wordDocument.AddMainDocumentPart();
            new Document(new Body()).Save(mainPart);

            var body = mainPart.Document.Body;
            var props = new SectionProperties();
            body.AppendChild(props);
            var sectionProperties = mainPart.Document
                .Body
                .GetFirstChild<SectionProperties>();
            sectionProperties.AppendChild(new PageSize()
            {
                Width = 8395U,
                Height = 5947U,
                Orient = PageOrientationValues.Landscape
            });
            foreach (var donor in people)
            {
                CreateBloodDonorAddressPage(body, donor);
                CreateInvitationMessagePage(body);
            }

            mainPart.Document.Save();

            wordDocument.Save();
        }
        return new ExportResultDto(FileName, mem.ToArray(), ContentType);
    }
    
    private void CreateInvitationMessagePage(Body body)
    {
        var cityAssociationParagraphProperties = new ParagraphProperties
        {
            KeepNext = new KeepNext(),
            FrameProperties = new FrameProperties
            {
                HeightType = HeightRuleValues.Exact,
                Height = 2300U,
                Width = "7200",
                Wrap = TextWrappingValues.Auto,
                HorizontalPosition = HorizontalAnchorValues.Page,
                VerticalPosition = VerticalAnchorValues.Page,
                Y = "2410",
                X = "666",
            },
            SpacingBetweenLines = new SpacingBetweenLines { Line = "480" },
            Justification = new Justification(){Val = JustificationValues.Center}
        };
        var invitationTexts = "Pozivamo vas na otvaranje naseg novog trgovackog centra".Split("\n");
        var invitationRuns = new List<OpenXmlElement>();
        foreach (var item in invitationTexts)
        {
            invitationRuns.Add(CreateRun(item));
            invitationRuns.Add(LineBreak());
        }
        
        var cityAssociationParagraph = new Paragraph(invitationRuns)
        {
            ParagraphProperties = cityAssociationParagraphProperties
        };
        body.Append(cityAssociationParagraph);
        body.Append(PageBreak());
    }

    private static void CreateBloodDonorAddressPage(Body body,
        Person donor)
    {
        var cityAssociationParagraphProperties = new ParagraphProperties
        {
            KeepNext = new KeepNext(),
            FrameProperties = new FrameProperties
            {
                HeightType = HeightRuleValues.Exact,
                Height = 766U,
                Width = "2500",
                Wrap = TextWrappingValues.Auto,
                HorizontalPosition = HorizontalAnchorValues.Page,
                VerticalPosition = VerticalAnchorValues.Page,
                Y = "990",
                X = "666"
            },
        };

        var cityAssociationRun = CreateRun($"Umag");
        var cityAssociationParagraph = new Paragraph(cityAssociationRun)
        {
            ParagraphProperties = cityAssociationParagraphProperties
        };
        body.Append(cityAssociationParagraph);

        var bloodDonorParagraphProperties = new ParagraphProperties
        {
            KeepNext = new KeepNext(),
            FrameProperties = new FrameProperties
            {
                HeightType = HeightRuleValues.Exact,
                Height = 2300U,
                Width = "3700",
                Wrap = TextWrappingValues.Auto,
                HorizontalPosition = HorizontalAnchorValues.Page,
                VerticalPosition = VerticalAnchorValues.Page,
                Y = "2810",
                X = "4080"
            },
            SpacingBetweenLines = new SpacingBetweenLines {Line = "480"}
        };

        var nameRun = CreateRun($"{donor.FirstName} {donor.LastName}");
        var contact = donor.Contacts.FirstOrDefault();
        if (contact != null)
        {
            var addressRun = CreateRun($"{contact.Address}");
            var bloodDonorParagraph = new Paragraph(
                nameRun,
                LineBreak(),
                addressRun)
            {
                ParagraphProperties = bloodDonorParagraphProperties
            };
            body.Append(bloodDonorParagraph);
            body.Append(PageBreak());    
        }
    }

    private static Run CreateRun(string text)
    {
        var run = new Run();
        run.AppendChild(new RunProperties(new Bold()));
        run.AppendChild(new RunProperties(new FontSize() { Val = "24" }));
        run.AppendChild(new Text(text));
        return run;
    }

    private static Run LineBreak()
    {
        return new Run(new Break { Type = BreakValues.TextWrapping });
    }

    private static Paragraph PageBreak()
    {
        return new Paragraph(new Run(new Break { Type = BreakValues.Page }));
    }
}