namespace Actonymous.API.Gateway.Settings.ExportReport.Domain.DTOs;

using Actonymous.Core.DAL.BaseEntities;

using JetBrains.Annotations;

[PublicAPI]
public sealed record TemplateSettingsDto
{
    public CustomerInfoDto CustomerInfoDto { get; set; } = null!;

    public ExecutorInfoDto ExecutorInfoDto { get; set; } = null!;

    public ContractInfoDto ContractInfoDto { get; set; } = null!;

    public TemplateFilesInfoDto TemplateFilesInfoDto { get; set; } = null!;
}