namespace Actonymous.API.ReportGenerationSaga.Services.Saga;

using System;
using System.Threading.Tasks;

using Actonymous.API.ReportGenerationSaga.PublicDTOs.DTOs;

using JetBrains.Annotations;

using MassTransit;

[UsedImplicitly]
public class ReportGenerationSaga: ISaga, InitiatedByOrOrchestrates<ReportGenerationRequest>
{
    public Guid CorrelationId { get; set; }

    public Task Consume(ConsumeContext<ReportGenerationRequest> context)
    {
        CorrelationId = context.Message.CorrelationId;
        
        return Task.CompletedTask;
    }
}