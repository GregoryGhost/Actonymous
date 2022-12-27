namespace Actonymous.API.ReportGenerationSaga.PublicDTOs.DTOs;

using System;

using JetBrains.Annotations;

using MassTransit;

[PublicAPI]
public record ReportGenerationRequest : CorrelatedBy<Guid>
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public Guid CorrelationId { get; }
}