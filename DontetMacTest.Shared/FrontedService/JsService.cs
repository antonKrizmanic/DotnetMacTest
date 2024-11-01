using Microsoft.JSInterop;

namespace DontetMacTest.Shared.FrontedService;

public class JsService(IJSRuntime iJsRuntime) : IJsService
{
    public async Task SaveAsExcelFile(string fileName, string byteBase64) => await iJsRuntime.InvokeVoidAsync("functions.saveAsExcelFile", fileName, byteBase64);
    public async Task SaveAsWordFile(string fileName, string byteBase64) => await iJsRuntime.InvokeVoidAsync("functions.saveAsWordFile", fileName, byteBase64);
    public async Task SaveAsPdfFile(string fileName, string byteBase64) =>
        await iJsRuntime.InvokeVoidAsync("functions.saveAsPdfFile", fileName, byteBase64);
}