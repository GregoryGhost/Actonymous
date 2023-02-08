namespace Actonymous.API.DocsPackagerStorage.Services;

using DocsPackager.V1;

using Grpc.Core;

using JetBrains.Annotations;

[PublicAPI]
public sealed class DocsPackagerService : DocsPackager.DocsPackagerBase
{
    private readonly DocsPackageDto _emptyDocsPackage;

    private readonly SavedDocsPackageDto _emptySavedDocsPackage;

    public DocsPackagerService()
    {
        _emptyDocsPackage = new DocsPackageDto();
        _emptySavedDocsPackage = new SavedDocsPackageDto();
    }

    /// <inheritdoc />
    public override async Task GetPackage(IAsyncStreamReader<GettingDocsPackageDto> requestStream,
        IServerStreamWriter<DocsPackageDto> responseStream, ServerCallContext context)
    {
        await foreach (var _ in requestStream.ReadAllAsync(context.CancellationToken))
        {
            await NotifyGettingPackageEndAsync(responseStream, _emptyDocsPackage);
        }
    }

    public override async Task SavePackage(IAsyncStreamReader<SavingDocsPackageDto> requestStream,
        IServerStreamWriter<SavedDocsPackageDto> responseStream, ServerCallContext context)
    {
        await foreach (var _ in requestStream.ReadAllAsync(context.CancellationToken))
        {
            await NotifySavingPackageEndAsync(responseStream, _emptySavedDocsPackage);
        }
    }

    private static async Task NotifyGettingPackageEndAsync(IServerStreamWriter<DocsPackageDto> responseStream,
        DocsPackageDto docsPackage)
    {
        await responseStream.WriteAsync(docsPackage);
    }

    private static async Task NotifySavingPackageEndAsync(IServerStreamWriter<SavedDocsPackageDto> responseStream,
        SavedDocsPackageDto docsPackage)
    {
        await responseStream.WriteAsync(docsPackage);
    }
}