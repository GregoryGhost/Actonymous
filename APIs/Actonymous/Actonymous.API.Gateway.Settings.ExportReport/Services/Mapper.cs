namespace Actonymous.API.Gateway.Settings.ExportReport.Services;

using Actonymous.API.Gateway.Settings.ExportReport.Domain.DTOs;
using Actonymous.API.Gateway.Settings.ExportReport.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public class Mapper
{
    public ExportReportSettingsDto MapSavingExportReportSettingsEntityDto(SavingExportReportSettingsDto dto)
    {
        return new ExportReportSettingsDto
        {
            Id = dto.Id,
            JiraCredentials = dto.JiraCredentials,
            TemplateSettings = dto.TemplateSettings,
            MorpherSettings = dto.MorpherSettings
        };
    }
    
    public SavedExportReportSettingsDto MapSavedExportReportSettingsDto(ExportReportSettingsDto dto)
    {
        return new SavedExportReportSettingsDto
        {
            Id = dto.Id,
            JiraCredentials = dto.JiraCredentials,
            TemplateSettings = dto.TemplateSettings,
            MorpherSettings = dto.MorpherSettings
        };
    }
}