namespace Actonymous.API.Gateway;

using Actonymous.API.Gateway.Settings.ExportReport.ModuleSettings;
using Actonymous.API.Gateway.Shared.Services.Pagination;
using Actonymous.Core.Module.Extensions;

using Grpc.Net.ClientFactory;

public static class StartupDi
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        RegisterGraphQlModules(builder);
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
}