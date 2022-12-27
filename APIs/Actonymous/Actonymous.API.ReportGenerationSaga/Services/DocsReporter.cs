namespace Actonymous.API.ReportGenerationSaga.Services;

using global::DocsReporter.V1;

using JetBrains.Annotations;

using MassTransit;

[UsedImplicitly]
public sealed class DocsReporter : IConsumer<UserReportingDataDto>
{
    private readonly global::DocsReporter.V1.DocsReporter.DocsReporterClient _docsReporterClient;

    public DocsReporter(global::DocsReporter.V1.DocsReporter.DocsReporterClient docsReporterClient)
    {
        _docsReporterClient = docsReporterClient;
    }

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<UserReportingDataDto> context)
    {
        var response = await _docsReporterClient.GetReportDocsAsync(context.Message);
        await context.RespondAsync<ReportDocsInfoDto>(response);
    }
}