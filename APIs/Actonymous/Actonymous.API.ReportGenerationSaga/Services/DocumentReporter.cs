namespace Actonymous.API.ReportGenerationSaga.Services;

using System.Threading.Tasks;

using DocsReporter.V1;

using JetBrains.Annotations;

using MassTransit;

[UsedImplicitly]
public sealed class DocumentReporter : IConsumer<UserReportingDataDto>
{
    private readonly DocsReporter.DocsReporterClient _docsReporterClient;

    public DocumentReporter(DocsReporter.DocsReporterClient docsReporterClient)
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