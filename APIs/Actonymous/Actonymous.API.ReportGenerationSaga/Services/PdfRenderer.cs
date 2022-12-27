namespace Actonymous.API.ReportGenerationSaga.Services;

using Grpc.Core;

using JetBrains.Annotations;

using MassTransit;

[UsedImplicitly]
public sealed class PdfRenderer: IConsumer<PdfRenderDto>
{
    private readonly  _pdfRendererClient;
    
    private readonly IBus _bus;

    public PdfRenderer( pdfRendererClient, IBus bus)
    {
        _pdfRendererClient = pdfRendererClient;
        _bus = bus;
    }

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<PdfRenderDto> context)
    {
        using var call = _pdfRendererClient.GetUserWorklogs();
        
        await WriteAsync(call, context.Message);
        await ReadAsync(call);
            
        await call.RequestStream.CompleteAsync();
    }

    private async Task ReadAsync(AsyncDuplexStreamingCall<PdfRenderDto, PdfRenderedDocsDto> call)
    {
        await foreach (var response in call.ResponseStream.ReadAllAsync())
        {
            await _bus.Publish(response);
        }
    }

    private static async Task WriteAsync(AsyncDuplexStreamingCall<PdfRenderDto, PdfRenderedDocsDto> call,
        PdfRenderDto data)
    {
        await call.RequestStream.WriteAsync(data);
    }
}

internal record PdfRenderedDocsDto
{
}

public record PdfRenderDto
{
}