namespace Actonymous.API.Gateway.Settings.ExportReport.DAL.Entities;

using Actonymous.Core.DAL;
using Actonymous.Core.DAL.BaseEntities;

using JetBrains.Annotations;

[UsedImplicitly]
public class ExportReportSettings : BaseEntity
{
    public JiraCredentials JiraCredentials { get; set; } = null!;

    public TemplateSettings TemplateSettings { get; set; } = null!;

    public MorpherSettings MorpherSettings { get; set; } = null!;
}