namespace Actonymous.API.Gateway.Shared.DTOs.Pagination;

using HotChocolate;
using HotChocolate.Types;

using JetBrains.Annotations;

[UsedImplicitly]
public record PaginationSettings
{
    [GraphQLType(typeof(UnsignedIntType))]
    public uint Skip { get; init; }
    
    [GraphQLType(typeof(UnsignedIntType))]
    public uint Take { get; init; }
}