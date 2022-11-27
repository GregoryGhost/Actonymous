namespace Actonymous.API.Gateway.Settings.ExportReport.DAL.Entities;

using Actonymous.Core.DAL;
using Actonymous.Core.DAL.BaseEntities;

using JetBrains.Annotations;

[UsedImplicitly]
public class TemplateSettings : BaseEntity
{
    public CustomerInfo CustomerInfo { get; set; } = null!;

    public ExecutorInfo ExecutorInfo { get; set; } = null!;

    public ContractInfo ContractInfo { get; set; } = null!;

    public TemplateFilesInfo TemplateFilesInfo { get; set; } = null!;
}