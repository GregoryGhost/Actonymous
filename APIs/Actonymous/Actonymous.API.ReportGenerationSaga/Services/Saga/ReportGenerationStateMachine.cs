namespace Actonymous.API.ReportGenerationSaga.Services.Saga
{
    using System.Diagnostics;

    using Actonymous.API.ReportGenerationSaga.PublicDTOs.DTOs;

    using global::DocsReporter.V1;

    using Google.Protobuf.WellKnownTypes;

    using JetBrains.Annotations;

    using JiraWorklogManager.V1;

    using MassTransit;

    using UserWorklogInfoDto = JiraWorklogManager.V1.UserWorklogInfoDto;

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
        
        public Event<ExportedReportSettingsDto> ReadyExportedReportSettingsData { get; init; } = null!;
        public Event<UserWorklogInfoDto> ReadyUserWorklogData { get; init; } = null!;
        public Event<PdfRenderedDocsDto> ReadyPdfRenderData { get; init; } = null!;
        public Event<DocsPackageInfoDto> ReadyDocsPackageData { get; init; } = null!;

        public Request<ReportGenerationSagaState, ExportingReportSettingsDto, Empty> ExportReportSettingsRequest
        {
            get;
            init;
        } = null!;
        
        public Request<ReportGenerationSagaState, UserWorklogDto, Empty> GetUserWorklogsRequest
        {
            get;
            init;
        } = null!;
        
        public Request<ReportGenerationSagaState, UserReportingDataDto, ReportDocsInfoDto> GetDocsReportRequest
        {
            get;
            init;
        } = null!;
        
        public Request<ReportGenerationSagaState, PdfRenderDto, Empty> GetPdfRenderRequest
        {
            get;
            init;
        } = null!;
        
        public Request<ReportGenerationSagaState, DocsPackageDto, Empty> GetDocsPackageRequest
        {
            get;
            init;
        } = null!;
        
        private static async Task RespondFromSagaAsync(SagaConsumeContext<ReportGenerationSagaState> context, string error)
        {
            if (context.Saga.ResponseAddress is null)
            {
                throw new Exception($"Provide {context.Saga.ResponseAddress} to send data to endpoint.");
            }
            var endpoint = await context.GetSendEndpoint(context.Saga.ResponseAddress);
            await endpoint.Send(
                new ReportGenerationResponse
                {
                    ReportPackageLink = context.Saga.ReportPackageLink,
                    ErrorMessage = error
                },
                r => r.RequestId = context.Saga.RequestId);
        }
    }
}