namespace Actonymous.API.ReportGenerationSaga.Services.Saga
{
    using System;

    using JetBrains.Annotations;

    [UsedImplicitly]
    public sealed record ReportGenerationSagaState : MassTransit.SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        
        public ulong RestaurantId { get; set; }
        
        public ulong SynchronizationRestaurantRequestId { get; set; }

        public string? CurrentState { get; set; }
        public Guid? RequestId { get; set; }

        public Uri? ResponseAddress { get; set; }
    }
}