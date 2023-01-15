namespace Actonymous.API.ReportSettingsExporter.DAL.Entities;

using Actonymous.Core.DAL.BaseEntities;

using JetBrains.Annotations;

[UsedImplicitly]
public class TemplateFilesInfo : BaseEntity
{
    public byte[] ActTemplateFile { get; set; } = Array.Empty<byte>();
    
    public byte[] TaskTemplateFile { get; set; } = Array.Empty<byte>();
}