namespace Actonymous.API.ReportSettingsExporter.Domain.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public sealed record JiraCredentialsDto
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ServerAddress { get; set; } = null!;
}