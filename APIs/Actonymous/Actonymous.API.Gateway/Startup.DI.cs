namespace Actonymous.API.Gateway;

using Actonymous.API.Gateway.Settings.ExportReport.APIs;
using Actonymous.API.Gateway.Shared.Services.Pagination;

using Grpc.Net.ClientFactory;

using JiraWorklogManager.V1;

public static class StartupDi
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        RegisterGraphQlTypes(builder);
        RegisterGrpcClients(builder);

        RegisterStaffServices(builder);

        return builder;
    }
    
    private static void RegisterStaffServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<Paginator>();
    }

    private static void RegisterGraphQlTypes(WebApplicationBuilder builder)
    {
        builder.Services
               .AddGraphQLServer()
               .AddInMemorySubscriptions()
           
               .AddQueryType(t => t.Name("Query"))
               .AddTypeExtension<Query>()
           
               .AddMutationType(t => t.Name("Mutation"))
               .AddTypeExtension<Mutation>()
           
               .AddSubscriptionType(t => t.Name("Subscription"));
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