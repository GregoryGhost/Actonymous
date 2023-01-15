namespace Actonymous.API.ReportSettingsExporter.Domain.Services;

using global::ReportSettingsExporter.V1;

public interface IReportSettingsExporterDataService
{
    Task<ReportSettingsDto> GetAsync();
    Task<SavedReportSettingsDto> SaveAsync(SavingReportSettingsDto data, CancellationToken cancellationToken = default);
}