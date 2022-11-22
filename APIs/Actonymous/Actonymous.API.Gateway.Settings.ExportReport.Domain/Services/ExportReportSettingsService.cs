namespace Actonymous.API.Gateway.Settings.ExportReport.Domain.Services;

using Actonymous.API.Gateway.Settings.ExportReport.DAL;
using Actonymous.API.Gateway.Settings.ExportReport.DAL.Entities;
using Actonymous.API.Gateway.Settings.ExportReport.Domain.DTOs;
using Actonymous.Core.DAL.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public class ExportReportSettingsService
{
    private readonly AppDbContext _dbContext;

    private readonly Mapper _mapper;

    public ExportReportSettingsService(AppDbContext dbContext, Mapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task SaveAsync(ExportReportSettingsDto dto, CancellationToken cancellationToken = default)
    {
        var entity = _mapper.MapToEntityExportReportSettings(dto);
        await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ExportReportSettingsDto?> GetAsync(BaseRecordDto dto)
    {
        var foundEntity = await _dbContext.FindAsync<ExportReportSettings>(dto.Id).ConfigureAwait(false);
        if (foundEntity is null)
        {
            return null;
        }

        var foundRecord = _mapper.MapFromEntityExportReportSettings(foundEntity);

        return foundRecord;
    }
}