namespace Actonymous.API.ReportSettingsExporter.Services;

using DocsReportSettingsExporter.V1;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using JetBrains.Annotations;

[PublicAPI]
public sealed class ReportSettingsExporterService : ReportSettingsExporter.ReportSettingsExporterBase
{
    private readonly IReportSettingsExporterDataService _dataService;

    public ReportSettingsExporterService(IReportSettingsExporterDataService dataService)
    {
        _dataService = dataService;
    }

    /// <inheritdoc />
    public override async Task GetSettings(IAsyncStreamReader<Empty> requestStream, IServerStreamWriter<ReportSettingsDto> responseStream, ServerCallContext context)
    {
        await foreach (var _ in requestStream.ReadAllAsync(context.CancellationToken))
        {
            var settings = await _dataService.GetAsync();
            await NotifyGettingSettingsEndAsync(responseStream, settings);
        }
    }

    private static async Task NotifyGettingSettingsEndAsync(IServerStreamWriter<ReportSettingsDto> responseStream, ReportSettingsDto settings)
    {
        await responseStream.WriteAsync(settings);
    }

    /// <inheritdoc />
    public override async Task<SavedReportSettingsDto> SaveSettings(SavingReportSettingsDto request, ServerCallContext context)
    {
        return await _dataService.SaveAsync(request);
    }
}