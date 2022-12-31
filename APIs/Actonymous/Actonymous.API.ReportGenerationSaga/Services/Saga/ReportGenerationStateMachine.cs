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
            Event(
                () => GenerateReport,
                x =>
                    x.CorrelateBy((instance, context) => instance.RestaurantId == context.Message.)
                     .SelectId(y => NewId.NextGuid()));
            Event(
                () => ReadyReportSettingsData,
                x =>
                    x.CorrelateBy((instance, context) => instance.RestaurantId == context.Message.RestaurantId)
                     .SelectId(y => NewId.NextGuid()));
            Event(
                () => ReadyUserWorklogData,
                x =>
                    x.CorrelateBy((instance, context) => instance.RestaurantId == context.Message.RequestId));
            Event(
                () => ReadyPdfRenderData,
                x =>
                    x.CorrelateBy((instance, context) => instance.RestaurantId == context.Message.RequestId));
            Event(
                () => ReadyPdfRenderData,
                x =>
                    x.CorrelateBy((instance, context) => instance.RestaurantId == context.Message.RequestId));
            Event(
                () => ReadyDocsPackageData,
                x =>
                    x.CorrelateBy((instance, context) => instance.RestaurantId == context.Message.RequestId));
            Request(() => ExportReportSettingsRequest);
            Request(() => GetUserWorklogsRequest);
            Request(() => GetDocsReportRequest);
            Request(() => GetPdfRenderRequest);
            Request(() => GetDocsPackageRequest);

            Initially(
                When(GenerateReport)
                    .Then(
                        x =>
                        {
                            if (!x.TryGetPayload(
                                    out SagaConsumeContext<ReportGenerationSagaState, ReportGenerationRequest>? payload))
                                throw new Exception("Unable to retrieve required payload for callback data.");

                            Debug.Assert(payload != null, nameof(payload) + " != null");

                            x.Saga.RequestId = payload.RequestId;
                            x.Saga.ResponseAddress = payload.ResponseAddress;
                            x.Saga.RestaurantId = payload.Message.RestaurantId;
                        })
                    .Request(
                        ExportReportSettingsRequest,
                        x =>
                        {
                            var data = new ExportingReportSettingsDto
                            {
                                //TOOD: init fields
                            };

                            return x.Init<ExportingReportSettingsDto>(data);
                        })
                    .TransitionTo(ExportingReportSettings)
            );

            During(
                ExportingReportSettings,
                When(ReadyReportSettingsData)
                    .Request(
                        GetUserWorklogsRequest,
                        x =>
                        {
                            var data = new UserWorklogDto
                            {
                                //TODO: init fields
                            };

                            return x.Init<UserWorklogDto>(data);
                        })
                    .TransitionTo(ExportingUserWorklogs)
            );

            During(
                ExportingUserWorklogs,
                When(ReadyUserWorklogData)
                    .Request(
                        GetDocsReportRequest,
                        x =>
                        {
                            var data = new UserReportingDataDto
                            {
                                //TODO: init fields
                            };

                            return x.Init<UserReportingDataDto>(data);
                        })
                    .TransitionTo(GetDocsReportRequest.Pending)
            );

            During(
                GetDocsReportRequest.Pending,
                When(GetDocsReportRequest.Completed)
                    .Request(
                        GetPdfRenderRequest,
                        x =>
                        {
                            var data = new PdfRenderDto
                            {
                                //TODO: init fields
                            };

                            return x.Init<PdfRenderDto>(data);
                        })
                    .TransitionTo(MakingPdfRenderData),
                When(GetDocsReportRequest.Faulted)
                    .ThenAsync(
                        async context =>
                        {
                            const string ErrorOnGetDocsReport = "Error on get docs report.";
                            var error = GetFormattedError(ErrorOnGetDocsReport, context);
                            await RespondFromSagaAsync(context, error);
                        })
                    .TransitionTo(Failed),
                When(GetDocsReportRequest.TimeoutExpired)
                    .ThenAsync(async context =>
                    {
                        await RespondFromSagaAsync(context, "Timeout expired on get docs report.");
                    })
                    .TransitionTo(Failed)
            );

            During(
                MakingPdfRenderData,
                When(ReadyPdfRenderData)
                    .Request(
                        GetDocsPackageRequest,
                        x =>
                        {
                            var data = new DocsPackageDto
                            {
                                //TODO: init fields
                            };

                            return x.Init<DocsPackageDto>(data);
                        })
                    .TransitionTo(PackingDocsPackage)
            );

            During(
                PackingDocsPackage,
                When(ReadyDocsPackageData)
                    .ThenAsync(async context => { await RespondFromSagaAsync(context, string.Empty); })
                    .TransitionTo(PackedDocsPackage)
                    .Finalize()
            );
        }

        public State ExportingReportSettings { get; init; } = null!;

        public State ExportingUserWorklogs { get; init; } = null!;

        public State Failed { get; init; } = null!;

        public Request<ReportGenerationSagaState, ExportingReportSettingsDto, Empty> ExportReportSettingsRequest { get; init; } =
            null!;

        public Event<ReportGenerationRequest> GenerateReport { get; init; } = null!;

        public Request<ReportGenerationSagaState, DocsPackageDto, Empty> GetDocsPackageRequest { get; init; } = null!;

        public Request<ReportGenerationSagaState, UserReportingDataDto, ReportDocsInfoDto> GetDocsReportRequest { get; init; } =
            null!;

        public Request<ReportGenerationSagaState, PdfRenderDto, Empty> GetPdfRenderRequest { get; init; } = null!;

        public Request<ReportGenerationSagaState, UserWorklogDto, Empty> GetUserWorklogsRequest { get; init; } = null!;

        public State MakingPdfRenderData { get; init; } = null!;

        public State PackedDocsPackage { get; init; } = null!;

        public State PackingDocsPackage { get; init; } = null!;

        public Event<DocsPackageInfoDto> ReadyDocsPackageData { get; init; } = null!;

        public Event<PdfRenderedDocsDto> ReadyPdfRenderData { get; init; } = null!;

        public Event<ExportedReportSettingsDto> ReadyReportSettingsData { get; init; } = null!;

        public Event<UserWorklogInfoDto> ReadyUserWorklogData { get; init; } = null!;

        private static string GetFormattedError<T>(string error, ConsumeContext<Fault<T>> context)
        {
            var errors = string.Join("; ", context.Message.Exceptions.Select(x => x.Message));
            var formattedError = $"{error}{errors}";

            return formattedError;
        }

        private static async Task RespondFromSagaAsync(SagaConsumeContext<ReportGenerationSagaState> context, string error)
        {
            if (context.Saga.ResponseAddress is null)
                throw new Exception($"Provide {context.Saga.ResponseAddress} to send data to endpoint.");
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