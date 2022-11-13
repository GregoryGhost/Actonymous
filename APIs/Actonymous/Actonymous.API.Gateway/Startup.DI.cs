namespace Actonymous.API.Gateway;

using Actonymous.API.Gateway.Shared.Services.Pagination;

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
               .AddTypeExtension<ProductsQuery>()
               .AddTypeExtension<ProductsSynchronizationQuery>()
           
               .AddMutationType(t => t.Name("Mutation"))
               .AddTypeExtension<ProductsMutation>()
               .AddTypeExtension<ProductsSynchronizationMutation>()
           
               .AddSubscriptionType(t => t.Name("Subscription"))
               .AddTypeExtension<ProductsSubscription>()
               .AddTypeExtension<ProductsSycnhronizationSubscription>();
    }

    private static void RegisterGrpcClients(WebApplicationBuilder builder)
    {
        builder.Services.AddGrpcClient<Merchandiser.V1.Merchandiser.MerchandiserClient>(
                   options =>
                   {
                       var jiraWorklogManagerAddress = Environment.GetEnvironmentVariable("API_JIRA_WORKLOG_MANAGER_ADDRESS");
                       if (string.IsNullOrWhiteSpace(jiraWorklogManagerAddress))
                           throw new Exception("You must provide correct merchandiser address.");

                       options.Address = new Uri(jiraWorklogManagerAddress);
                   })
               .ConfigureChannel(
                   options => { options.UnsafeUseInsecureChannelCallCredentials = true; });
    }
}