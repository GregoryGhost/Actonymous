namespace Actonymous.API.ReportSettingsExporter.DAL.Entities;

using Actonymous.Core.DAL.BaseEntities;

using JetBrains.Annotations;

[UsedImplicitly]
public class JiraCredentials : BaseEntity
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ServerAddress { get; set; } = null!;
}