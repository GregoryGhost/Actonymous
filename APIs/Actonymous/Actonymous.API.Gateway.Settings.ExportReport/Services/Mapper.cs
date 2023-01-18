namespace Actonymous.API.Gateway.Settings.ExportReport.Services;

using Actonymous.API.Gateway.Settings.ExportReport.DTOs;

using DocsReportSettingsExporter.V1;

using JetBrains.Annotations;

[PublicAPI]
public class Mapper
{
    public SavingReportSettingsDto MapSavingExportReportSettingsEntityDto(SavingExportReportSettingsDto dto)
    {
        var settingsId = (ulong) dto.Id;
        
        return new SavingReportSettingsDto
        {
            Id = settingsId,
            JiraCredentials = dto.JiraCredentials,
            TemplateSettings = dto.TemplateSettings,
            MorpherSettings = dto.MorpherSettings
        };
    }
    
    public SavedExportReportSettingsDto MapSavedExportReportSettingsDto(ReportSettingsDto dto)
    {
        var settingsId = (long) dto.Id;
        
        return new SavedExportReportSettingsDto
        {
            Id = settingsId,
            JiraCredentials = dto.JiraCredentials,
            TemplateSettings = dto.TemplateSettings,
            MorpherSettings = dto.MorpherSettings
        };
    }
}