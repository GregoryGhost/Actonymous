namespace Actonymous.API.ReportGenerationSaga;

using System.Reflection;

using Actonymous.Core.Module.Extensions;

using DocsReporter.V1;

using Grpc.Net.ClientFactory;

using JiraWorklogManager.V1;

using MassTransit;

public static class StartupDi
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        RegisterGrpcClients(builder);
        RegisterMassTransitDependencies(builder);
        RegisterStaffServices(builder);

        return builder;
    }

    private static void RegisterDocsReporterClient(WebApplicationBuilder builder)
    {
        builder.Services.AddGrpcClient<DocsReporter.DocsReporterClient>(SetDocsReporterClientOptions)
               .ConfigureChannel(
                   options => { options.UnsafeUseInsecureChannelCallCredentials = true; });
    }

    private static void RegisterGrpcClients(WebApplicationBuilder builder)
    {
        RegisterJiraWorklogManagerClient(builder);
        RegisterDocsReporterClient(builder);
    }

    private static void RegisterJiraWorklogManagerClient(WebApplicationBuilder builder)
    {
        builder.Services.AddGrpcClient<JiraWorklogManager.JiraWorklogManagerClient>(SetJiraWorklogManagerClientOptions)
               .ConfigureChannel(
                   options => { options.UnsafeUseInsecureChannelCallCredentials = true; });
    }

    private static void RegisterMassTransitDependencies(WebApplicationBuilder builder)
    {
        var rabbitMqConfig = ConfigHelper.GetRabbitMqConfig();
        var isRunningInContainer = ConfigHelper.CheckRunningInContainer();

        builder.Services.AddMassTransit(
            configurator =>
            {
                configurator.AddDelayedMessageScheduler();

                configurator.SetKebabCaseEndpointNameFormatter();

                // By default, sagas are in-memory, but should be changed to a durable
                // saga repository.
                configurator.SetInMemorySagaRepositoryProvider();

                var entryAssembly = Assembly.GetEntryAssembly();

                configurator.AddConsumers(entryAssembly);
                configurator.AddSagaStateMachines(entryAssembly);
                configurator.AddSagas(entryAssembly);
                configurator.AddActivities(entryAssembly);

                configurator.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        if (isRunningInContainer)
                            cfg.Host("rabbitmq");
                        else
                            cfg.Host(
                                rabbitMqConfig.Address,
                                "/",
                                h =>
                                {
                                    h.Username(rabbitMqConfig.Login);
                                    h.Password(rabbitMqConfig.Password);
                                });

                        cfg.UseDelayedMessageScheduler();

                        cfg.ConfigureEndpoints(context);
                    });
            });
    }

    private static void RegisterStaffServices(WebApplicationBuilder builder)
    {
    }

    private static void SetDocsReporterClientOptions(GrpcClientFactoryOptions options)
    {
        StartupDiHelper.SetClientOptions("DOCS_REPORTER_ADDRESS", options);
    }

    private static void SetJiraWorklogManagerClientOptions(GrpcClientFactoryOptions options)
    {
        StartupDiHelper.SetClientOptions("JIRA_WORKLOG_MANAGER_ADDRESS", options);
    }
}