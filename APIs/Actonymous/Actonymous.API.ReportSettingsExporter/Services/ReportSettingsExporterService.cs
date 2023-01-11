namespace Actonymous.API.ReportSettingsExporter.Services;

using global::ReportSettingsExporter.V1;

using Google.Protobuf.WellKnownTypes;

using Grpc.Core;

using JetBrains.Annotations;

[PublicAPI]
public sealed class ReportSettingsExporterService : ReportSettingsExporter.ReportSettingsExporterBase
{
    /// <inheritdoc />
    public override Task GetSettings(IAsyncStreamReader<Empty> requestStream, IServerStreamWriter<ReportSettingsDto> responseStream, ServerCallContext context)
    {
        return base.GetSettings(requestStream, responseStream, context);
    }

    /// <inheritdoc />
    public override Task<SavedReportSettingsDto> SaveSettings(SavingReportSettingsDto request, ServerCallContext context)
    {
        return base.SaveSettings(request, context);
    }
}