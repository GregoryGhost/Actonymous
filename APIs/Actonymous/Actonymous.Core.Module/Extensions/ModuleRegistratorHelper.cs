namespace Actonymous.Core.Module.Extensions;

using Actonymous.Core.Module.Contracts;

using HotChocolate.Execution.Configuration;

public static class ModuleRegistratorHelper
{
    public static void RegisterModules(this IEnumerable<IGraphQlModuleSettings> modules, IRequestExecutorBuilder graphQlServer)
    {
        foreach (var module in modules)
        {
            module.RegisterModule(graphQlServer);
        }
    }
}