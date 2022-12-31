namespace Actonymous.API.ReportGenerationSaga.Services;

using System.Threading.Tasks;

using DocsPackager.V1;

using Grpc.Core;

using JetBrains.Annotations;

using MassTransit;

[UsedImplicitly]
public sealed class DocumentPackager : IConsumer<SavingDocsPackageDto>
{
    private readonly IBus _bus;

    private readonly DocsPackager.DocsPackagerClient _docsPackageClient;

    public DocumentPackager(IBus bus, DocsPackager.DocsPackagerClient docsPackageClient)
    {
        _bus = bus;
        _docsPackageClient = docsPackageClient;
    }

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<SavingDocsPackageDto> context)
    {
        using var call = _docsPackageClient.SavePackage();

        await WriteAsync(call, context.Message);
        await ReadAsync(call);

        await call.RequestStream.CompleteAsync();
    }

    private async Task ReadAsync(AsyncDuplexStreamingCall<SavingDocsPackageDto, SavedDocsPackageDto> call)
    {
        await foreach (var response in call.ResponseStream.ReadAllAsync())
        {
            await _bus.Publish(response);
        }
    }

    private static async Task WriteAsync(AsyncDuplexStreamingCall<SavingDocsPackageDto, SavedDocsPackageDto> call,
        SavingDocsPackageDto data)
    {
        await call.RequestStream.WriteAsync(data);
    }
}