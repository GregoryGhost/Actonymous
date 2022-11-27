namespace Actonymous.API.Gateway.Settings.ExportReport.Domain.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public sealed record ExecutorInfoDto
{
    public string CompanyName { get; set; } = null!;

    public string HeaderFullname { get; set; } = null!;

    public string HeaderPosition { get; set; } = null!;
    
    public decimal RatePerHour { get; set; }
}