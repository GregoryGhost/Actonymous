namespace Actonymous.API.DocsRenderer.Services;

using Grpc.Core;

using JetBrains.Annotations;

using Tex2PdfRenderer.V1;

[PublicAPI]
public sealed class RenderService : Tex2PdfRenderer.Tex2PdfRendererBase
{
    private readonly PdfPackageDto _emptyPdfPackage;

    public RenderService()
    {
        _emptyPdfPackage = new PdfPackageDto();
    }
    
    /// <inheritdoc />
    public override async Task Render(IAsyncStreamReader<TexPackageDto> requestStream, IServerStreamWriter<PdfPackageDto> responseStream, ServerCallContext context)
    {
        await foreach (var _ in requestStream.ReadAllAsync(context.CancellationToken))
        {
            await NotifyGettingSettingsEndAsync(responseStream, _emptyPdfPackage);
        }
    }

    private static async Task NotifyGettingSettingsEndAsync(IServerStreamWriter<PdfPackageDto> responseStream, PdfPackageDto pdfPackage)
    {
        await responseStream.WriteAsync(pdfPackage);
    }
}