namespace Actonymous.API.ReportSettingsExporter.Services;

using DocsReportSettingsExporter.V1;

public interface IReportSettingsExporterDataService
{
    Task<ReportSettingsDto> GetAsync();
    Task<SavedReportSettingsDto> SaveAsync(SavingReportSettingsDto data, CancellationToken cancellationToken = default);
}