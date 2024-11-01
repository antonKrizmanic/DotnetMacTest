namespace DontetMacTest.Shared.FrontedService;

public interface IJsService
{
    Task SaveAsExcelFile(string fileName, string byteBase64);
    Task SaveAsWordFile(string fileName, string byteBase64);
    Task SaveAsPdfFile(string fileName, string byteBase64);
}