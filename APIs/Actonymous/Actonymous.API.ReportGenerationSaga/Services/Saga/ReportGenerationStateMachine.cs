namespace Actonymous.API.ReportGenerationSaga.Services.Saga
{
    using System.Diagnostics;

    using Google.Protobuf.WellKnownTypes;

    using JetBrains.Annotations;

    using MassTransit;

    [UsedImplicitly]
    public sealed class ReportGenerationStateMachine : MassTransitStateMachine<ReportGenerationSagaState>
    {
        private readonly ILogger<ReportGenerationStateMachine> _logger;
        
        public ReportGenerationStateMachine(ILogger<ReportGenerationStateMachine> logger)
        {
            _logger = logger;
            InstanceState(x => x.CurrentState);
            Event(() => SynchronizeRestaurantProducts, 
                x => 
                    x.CorrelateBy((instance, context) => instance.RestaurantId == context.Message.RestaurantId)
                     .SelectId(y => NewId.NextGuid()));
            Event(
                () => ScrappedRestaurantProducts,
                x =>
                    x.CorrelateBy((instance, context) => instance.RestaurantId == context.Message.RequestId));
            Event(
                () => GetSynchronizedRestaurant,
                x =>
                    x.CorrelateBy((instance, context) => instance.RestaurantId == context.Message.RequestId));
            Request(() => SynchronizeRestaurantRequest);
            Request(() => CreateSynchronizationRequest);

            Initially(

                When(SynchronizeRestaurantProducts)
                    .Then(
                        x =>
                        {
                            if (!x.TryGetPayload(out SagaConsumeContext<ReportGenerationSagaState, SynchronizeRestaurantProductsRequest>? payload))
                                throw new Exception("Unable to retrieve required payload for callback data.");

                            Debug.Assert(payload != null, nameof(payload) + " != null");
                            
                            x.Saga.RequestId = payload.RequestId;
                            x.Saga.ResponseAddress = payload.ResponseAddress;
                            x.Saga.RestaurantId = payload.Message.RestaurantId;
                        })
                    .Request(CreateSynchronizationRequest,
                        x =>
                        {
                            var data = new SynchronizationRestaurantRequest
                            {
                                RestaurantId = x.Saga.RestaurantId
                            };
                            
                            return x.Init<SynchronizationRestaurantRequest>(data);
                        })
                    .TransitionTo(CreateSynchronizationRequest.Pending)
            );
            
            During(
                CreateSynchronizationRequest.Pending,
                When(CreateSynchronizationRequest.Completed)
                    .Then(
                        x =>
                        {
                            x.Saga.SynchronizationRestaurantRequestId = x.Message.RequestId;
                        })
                    .TransitionTo(CreatedScrappingRestaurantProductsRequest)
                );

            During(
                CreatedScrappingRestaurantProductsRequest,
                When(ScrappedRestaurantProducts)
                    .Request(SynchronizeRestaurantRequest,
                        x =>
                        {
                            var data = new SynchronizingData
                            {
                                RequestId = x.Saga.SynchronizationRestaurantRequestId
                            };
                            
                            return x.Init<SynchronizingData>(data);
                        })
                    .TransitionTo(SynchronizingRestaurant)
            );

            During(
                SynchronizingRestaurant,
                When(GetSynchronizedRestaurant)
                    .TransitionTo(SynchronizedRestaurant)
                );

            During(
                SynchronizedRestaurant,
                When(SynchronizedRestaurant.Enter)
                    .ThenAsync(async context => { await RespondFromSagaAsync(context, string.Empty); })
                    .Finalize()
            );
        }

        //States
        public State GottenExportReportSettings { get; init; } = null!;
        
        // Events
        public Event<ExportedReportSettingsData> ReadyExportedReportSettingsData { get; init; } = null!;
        public Event<UserWorklogData> ReadyUserWorklogData { get; init; } = null!;
        public Event<DocsReportData> ReadyDocsReportData { get; init; } = null!;
        public Event<PdfRenderData> ReadyPdfRenderData { get; init; } = null!;
        public Event<DocsPackageData> ReadyDocsPackageData { get; init; } = null!;
        
        //Requests

        public Request<ReportGenerationSagaState, ExportingReportSettingsRequest, Empty> ExportReportSettingsRequest
        {
            get;
            init;
        } = null!;
        
        public Request<ReportGenerationSagaState, UserWorklogRequest, Empty> GetUserWorklogsRequest
        {
            get;
            init;
        } = null!;
        
        public Request<ReportGenerationSagaState, DocsReportRequest, Empty> GetDocsReportRequest
        {
            get;
            init;
        } = null!;
        
        public Request<ReportGenerationSagaState, PdfRenderRequest, Empty> GetPdfRenderRequest
        {
            get;
            init;
        } = null!;
        
        public Request<ReportGenerationSagaState, DocsPackageRequest, Empty> GetDocsPackageRequest
        {
            get;
            init;
        } = null!;
        ///------Below it's examples of describtion primitives.
            
        public State CreatedScrappingRestaurantProductsRequest { get; init; } = null!;

        public State Failed { get; init; } = null!;

        public State SynchronizedRestaurant { get; init; } = null!;

        public State SynchronizingRestaurant { get; init; } = null!;

        public Event<SynchronizeRestaurantProductsRequest> SynchronizeRestaurantProducts { get; init; } = null!;

        public Event<SynchronizedRestaurantProductsRequest> ScrappedRestaurantProducts { get; init; } = null!;

        public Event<SynchronizedData> GetSynchronizedRestaurant { get; init; } = null!;

        public Request<ReportGenerationSagaState, SynchronizingData, Empty> SynchronizeRestaurantRequest { get; init; } = null!;
        
        public Request<ReportGenerationSagaState, SynchronizationRestaurantRequest,
            CreatedSynchronizationRestaurantRequest> CreateSynchronizationRequest { get; init; } = null!;

        private static async Task RespondFromSagaAsync(SagaConsumeContext<ReportGenerationSagaState> context, string error)
        {
            if (context.Saga.ResponseAddress is null)
            {
                throw new Exception($"Provide {context.Saga.ResponseAddress} for send data to endpoint.");
            }
            var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
            await endpoint.Send(
                new SynchronizeRestaurantProductsResponse
                {
                    RestaurantId = context.Saga.RestaurantId,
                    ErrorMessage = error
                },
                r => r.RequestId = context.Saga.RequestId);
        }
    }
}