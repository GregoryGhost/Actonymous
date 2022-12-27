namespace Actonymous.API.Gateway.Report.ReportGeneration.DTOs;

public record ReportGenerationInfoDto
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
}