namespace DontetMacTest.Shared.DTOs;

public record ExportResultDto(string FileName, byte[] Data, string ContentType);