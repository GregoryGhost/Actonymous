namespace Actonymous.API.ReportSettingsExporter.DAL.Entities;

using Actonymous.Core.DAL.BaseEntities;

using JetBrains.Annotations;

[UsedImplicitly]
public class MorpherSettings : BaseEntity
{
    public string AccessToken { get; set; } = null!;
}