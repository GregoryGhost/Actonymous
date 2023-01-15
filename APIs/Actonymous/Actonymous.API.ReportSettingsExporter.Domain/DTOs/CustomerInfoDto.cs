namespace Actonymous.API.ReportSettingsExporter.Domain.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public sealed record CustomerInfoDto
{
    public string CompanyName { get; set; } = null!;

    public string HeaderFullname { get; set; } = null!;

    public string HeaderPosition { get; set; } = null!;
}