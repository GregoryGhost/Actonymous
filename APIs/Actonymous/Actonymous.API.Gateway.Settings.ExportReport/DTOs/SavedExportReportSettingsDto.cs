namespace Actonymous.API.Gateway.Settings.ExportReport.DTOs;

using Actonymous.API.Gateway.Settings.ExportReport.Domain.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public sealed record SavedExportReportSettingsDto
{
    public long Id { get; set; }
    
    public JiraCredentialsDto JiraCredentials { get; set; } = null!;

    public TemplateSettingsDto TemplateSettings { get; set; } = null!;

    public MorpherSettingsDto MorpherSettings { get; set; } = null!;
}