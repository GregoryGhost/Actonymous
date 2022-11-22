namespace Actonymous.API.Gateway.Settings.ExportReport.APIs;

using Actonymous.API.Gateway.Settings.ExportReport.Domain.Services;
using Actonymous.API.Gateway.Settings.ExportReport.DTOs;
using Actonymous.API.Gateway.Settings.ExportReport.Services;

using HotChocolate.Types;

using JetBrains.Annotations;

using Mapper = Actonymous.API.Gateway.Settings.ExportReport.Services.Mapper;

[PublicAPI]
[ExtendObjectType(Name = "Mutation")]
public class Mutation
{
    private readonly ExportReportSettingsService _exportReportSettingsService;

    private readonly Mapper _mapper;

    public Mutation(ExportReportSettingsService exportReportSettingsService, Mapper mapper)
    {
        _exportReportSettingsService = exportReportSettingsService;
        _mapper = mapper;
    }

    public async Task SaveExportReportSettingsRequest(SavingExportReportSettingsDto savingSettings, CancellationToken cancellationToken)
    {
        var savingExportReportSettings = _mapper.MapSavingExportReportSettingsEntityDto(savingSettings);
        await _exportReportSettingsService.SaveAsync(savingExportReportSettings, cancellationToken);
    }
}