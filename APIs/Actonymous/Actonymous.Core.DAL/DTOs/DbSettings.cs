namespace Actonymous.Core.DAL.DTOs;

public sealed record DbSettings
{
    public string ConnectionString { get; init; } = null!;
}