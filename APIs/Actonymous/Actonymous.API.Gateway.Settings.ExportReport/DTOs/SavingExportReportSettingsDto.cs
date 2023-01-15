namespace Actonymous.API.Gateway.Settings.ExportReport.DTOs;

using Actonymous.API.ReportSettingsExporter.Domain.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public sealed record SavingExportReportSettingsDto
{
    public long Id { get; set; }
    
    public JiraCredentialsDto JiraCredentials { get; set; } = null!;

    public TemplateSettingsDto TemplateSettings { get; set; } = null!;

    public MorpherSettingsDto MorpherSettings { get; set; } = null!;
}