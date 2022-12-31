namespace Actonymous.API.ReportGenerationSaga.Services;

using System.Threading.Tasks;

using Grpc.Core;

using JetBrains.Annotations;

using Tex2PdfRenderer.V1;

using MassTransit;

[UsedImplicitly]
public sealed class PdfRenderer: IConsumer<TexPackageDto>
{
    private readonly Tex2PdfRenderer.Tex2PdfRendererClient _pdfRendererClient;
    
    private readonly IBus _bus;

    public PdfRenderer(IBus bus, Tex2PdfRenderer.Tex2PdfRendererClient pdfRendererClient)
    {
        _bus = bus;
        _pdfRendererClient = pdfRendererClient;
    }

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<TexPackageDto> context)
    {
        using var call = _pdfRendererClient.Render();
        
        await WriteAsync(call, context.Message);
        await ReadAsync(call);
            
        await call.RequestStream.CompleteAsync();
    }

    private async Task ReadAsync(AsyncDuplexStreamingCall<TexPackageDto, PdfPackageDto> call)
    {
        await foreach (var response in call.ResponseStream.ReadAllAsync())
        {
            await _bus.Publish(response);
        }
    }

    private static async Task WriteAsync(AsyncDuplexStreamingCall<TexPackageDto, PdfPackageDto> call,
        TexPackageDto data)
    {
        await call.RequestStream.WriteAsync(data);
    }
}