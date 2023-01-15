namespace Actonymous.API.ReportSettingsExporter.DAL.Entities;

using Actonymous.Core.DAL.BaseEntities;

using JetBrains.Annotations;

[UsedImplicitly]
public class CustomerInfo : BaseEntity
{
    public string CompanyName { get; set; } = null!;

    public string HeaderFullname { get; set; } = null!;

    public string HeaderPosition { get; set; } = null!;
}