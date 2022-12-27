namespace Actonymous.Core.Module.DTOs;

public record RabbitMqConfig
{
    public string Address { get; init; } = null!;
    public string Login { get; init; } = null!;
    public string Password { get; init; } = null!;
}