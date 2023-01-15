namespace Actonymous.API.ReportSettingsExporter.Domain.Services;

using Actonymous.API.ReportSettingsExporter.DAL;
using Actonymous.API.ReportSettingsExporter.DAL.Entities;
using Actonymous.API.ReportSettingsExporter.Domain.DTOs;
using Actonymous.Core.DAL.DTOs;

using global::ReportSettingsExporter.V1;

using JetBrains.Annotations;

[PublicAPI]
public sealed class ExportReportSettingsService : IReportSettingsExporterDataService
{
    private readonly AppDbContext _dbContext;

    private readonly Mapper _mapper;

    public ExportReportSettingsService(AppDbContext dbContext, Mapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Task<ReportSettingsDto> GetAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SavedReportSettingsDto> SaveAsync(SavingReportSettingsDto data, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private async Task<ExportReportSettingsDto?> GetAsync(BaseRecordDto dto)
    {
        var foundEntity = await _dbContext.FindAsync<ExportReportSettings>(dto.Id).ConfigureAwait(false);
        if (foundEntity is null)
            return null;

        var foundRecord = _mapper.MapFromEntityExportReportSettings(foundEntity);

        return foundRecord;
    }

    private async Task SaveAsync(ExportReportSettingsDto dto, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.MapToEntityExportReportSettings(dto);
        await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}