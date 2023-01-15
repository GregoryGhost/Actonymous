namespace Actonymous.API.ReportSettingsExporter.DAL.Entities;

using Actonymous.Core.DAL.BaseEntities;

using JetBrains.Annotations;

[UsedImplicitly]
public class ContractInfo : BaseEntity
{
    public string ContractNumber { get; set; } = null!;
    
    public DateTime ApprovalDate { get; set; }
    
    public byte[] ContractFile { get; set; } = Array.Empty<byte>();
}