namespace Actonymous.API.Gateway.Settings.ExportReport.ModuleSettings;

using Actonymous.API.Gateway.Settings.ExportReport.APIs;
using Actonymous.Core.Module.Contracts;

using HotChocolate.Execution.Configuration;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

[PublicAPI]
public class ExportReportModuleSettings: IGraphQlModuleSettings
{
    /// <inheritdoc />
    public void RegisterModule(IRequestExecutorBuilder graphQlBuilder)
    {
        graphQlBuilder
               .AddQueryType(t => t.Name("Query"))
               .AddTypeExtension<Query>()
           
               .AddMutationType(t => t.Name("Mutation"))
               .AddTypeExtension<Mutation>()
           
               .AddSubscriptionType(t => t.Name("Subscription"));
    }
}