namespace Actonymous.API.ReportGenerationSaga.Services;

using Grpc.Core;

using JetBrains.Annotations;

using MassTransit;

[UsedImplicitly]
public sealed class DocsPackager: IConsumer<DocsPackageDto>
{
    private readonly  _docsPackageClient;
    
    private readonly IBus _bus;

    public PdfRenderer( docsPackageClient, IBus bus)
    {
        _docsPackageClient = docsPackageClient;
        _bus = bus;
    }

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<DocsPackageDto> context)
    {
        using var call = _docsPackageClient.GetUserWorklogs();
        
        await WriteAsync(call, context.Message);
        await ReadAsync(call);
            
        await call.RequestStream.CompleteAsync();
    }

    private async Task ReadAsync(AsyncDuplexStreamingCall<DocsPackageDto, DocsPackageInfoDto> call)
    {
        await foreach (var response in call.ResponseStream.ReadAllAsync())
        {
            await _bus.Publish(response);
        }
    }

    private static async Task WriteAsync(AsyncDuplexStreamingCall<DocsPackageDto, DocsPackageInfoDto> call,
        DocsPackageDto data)
    {
        await call.RequestStream.WriteAsync(data);
    }
}

public record DocsPackageInfoDto
{
}

public record DocsPackageDto
{
}