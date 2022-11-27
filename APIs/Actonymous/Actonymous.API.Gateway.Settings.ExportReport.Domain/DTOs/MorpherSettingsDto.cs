namespace Actonymous.API.Gateway.Settings.ExportReport.Domain.DTOs;

using Actonymous.Core.DAL.BaseEntities;

using JetBrains.Annotations;

[PublicAPI]
public sealed record MorpherSettingsDto
{
    public string AccessToken { get; set; } = null!;
}