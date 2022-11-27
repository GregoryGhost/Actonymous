namespace Actonymous.API.Gateway.Settings.ExportReport.Domain.DTOs;

using JetBrains.Annotations;

[PublicAPI]
public sealed record ContractInfoDto
{
    public string ContractNumber { get; set; } = null!;
    
    public DateTime ApprovalDate { get; set; }
    
    public byte[] ContractFile { get; set; } = Array.Empty<byte>();
}