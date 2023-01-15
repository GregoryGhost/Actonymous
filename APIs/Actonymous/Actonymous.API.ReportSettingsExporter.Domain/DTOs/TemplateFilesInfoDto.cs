namespace Actonymous.API.ReportSettingsExporter.Domain.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public sealed record TemplateFilesInfoDto
{
    public byte[] ActTemplateFile { get; set; } = Array.Empty<byte>();
    
    public byte[] TaskTemplateFile { get; set; } = Array.Empty<byte>();
}