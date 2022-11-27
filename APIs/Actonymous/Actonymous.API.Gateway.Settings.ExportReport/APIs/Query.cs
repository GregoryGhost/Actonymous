namespace Actonymous.API.Gateway.Settings.ExportReport.APIs;

using Actonymous.API.Gateway.Settings.ExportReport.Domain.Services;
using Actonymous.API.Gateway.Settings.ExportReport.DTOs;
using Actonymous.API.Gateway.Shared.Services.Pagination;
using Actonymous.Core.DAL.DTOs;

using HotChocolate.Types;

using JetBrains.Annotations;

using Mapper = Actonymous.API.Gateway.Settings.ExportReport.Services.Mapper;

[PublicAPI]
[ExtendObjectType("Query")]
public class Query
{
    private readonly Mapper _mapper;

    private readonly ExportReportSettingsService _exportReportSettingsService;

    public Query(Mapper mapper, ExportReportSettingsService exportReportSettingsService)
    {
        _mapper = mapper;
        _exportReportSettingsService = exportReportSettingsService;
    }

    [UseOffsetPaging]
    public async Task<SavedExportReportSettingsDto?> GetExportReportSettings(BaseRecordDto dto, CancellationToken cancellationToken)
    {
        var foundRecord = await _exportReportSettingsService.GetAsync(dto);
        if (foundRecord is null)
        {
            return null;
        }
        var mapped = _mapper.MapSavedExportReportSettingsDto(foundRecord);

        return mapped;
    }
}
