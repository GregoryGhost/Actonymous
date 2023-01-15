namespace Actonymous.API.ReportSettingsExporter.Domain.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public sealed record MorpherSettingsDto
{
    public string AccessToken { get; set; } = null!;
}