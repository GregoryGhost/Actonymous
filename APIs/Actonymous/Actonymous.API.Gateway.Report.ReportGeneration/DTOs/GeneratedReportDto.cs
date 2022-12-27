namespace Actonymous.API.Gateway.Report.ReportGeneration.DTOs;

public record GeneratedReportDto
{
    public IEnumerable<string> ReportFileUrls { get; init; } = Array.Empty<string>();
}