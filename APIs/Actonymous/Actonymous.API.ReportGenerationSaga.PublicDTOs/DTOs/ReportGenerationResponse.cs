namespace Actonymous.API.ReportGenerationSaga.PublicDTOs.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public record ReportGenerationResponse
{
    public string ReportPackageLink { get; init; } = null!;

    public string ErrorMessage { get; init; } = null!;
}