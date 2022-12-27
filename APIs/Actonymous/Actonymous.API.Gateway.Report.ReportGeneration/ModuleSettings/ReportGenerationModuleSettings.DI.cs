namespace Actonymous.API.Gateway.Report.ReportGeneration.ModuleSettings;

using Actonymous.API.Gateway.Report.ReportGeneration.APIs;
using Actonymous.Core.Module.Contracts;

using DocsReporter.V1;

using Grpc.Net.ClientFactory;

using HotChocolate.Execution.Configuration;

using JetBrains.Annotations;

using JiraWorklogManager.V1;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

[PublicAPI]
public class ReportGenerationModuleSettings : IGraphQlModuleSettings
{
    /// <inheritdoc />
    public void RegisterModule(IRequestExecutorBuilder graphQlBuilder)
    {
        graphQlBuilder
            .AddQueryType(t => t.Name("Query"))
            .AddTypeExtension<Query>()
            .AddMutationType(t => t.Name("Mutation"))
            .AddTypeExtension<Mutation>()
            .AddSubscriptionType(t => t.Name("Subscription"))
            .AddTypeExtension<Subscription>();
        
        //TODO: add JiraWorklogManagerService
        //RegisterStaffServices();
    }
}