namespace Actonymous.API.Gateway;

using Actonymous.API.Gateway.Settings.ExportReport.ModuleSettings;
using Actonymous.API.Gateway.Shared.Services.Pagination;
using Actonymous.Core.Module.Extensions;

using Grpc.Net.ClientFactory;

using JiraWorklogManager.V1;

public static class StartupDi
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        RegisterGraphQlModules(builder);
        RegisterGrpcClients(builder);

        RegisterStaffServices(builder);

        return builder;
    }
    
    private static void RegisterStaffServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<Paginator>();
    }

    private static void RegisterGraphQlModules(WebApplicationBuilder builder)
    {
        var graphQlServer = builder.Services
               .AddGraphQLServer()
               .AddInMemorySubscriptions();

        var modules = new[]
        {
            new ExportReportModuleSettings()
        };
        
        modules.RegisterModules(graphQlServer);
    }

    private static void RegisterGrpcClients(WebApplicationBuilder builder)
    {
        builder.Services.AddGrpcClient<JiraWorklogManager.JiraWorklogManagerClient>(SetJiraWorklogManagerClientOptions)
               .ConfigureChannel(
                   options => { options.UnsafeUseInsecureChannelCallCredentials = true; });
    }

    private static void SetJiraWorklogManagerClientOptions(GrpcClientFactoryOptions options)
    {
        SetClientOptions("JIRA_WORKLOG_MANAGER_ADDRESS", options);
    }

    private static void SetClientOptions(string environmentVariable, GrpcClientFactoryOptions options)
    {
        var apiAddress = Environment.GetEnvironmentVariable(environmentVariable);
        if (string.IsNullOrWhiteSpace(apiAddress))
            throw new Exception("You must provide correct  address.");

        options.Address = new Uri(apiAddress);
    }
}