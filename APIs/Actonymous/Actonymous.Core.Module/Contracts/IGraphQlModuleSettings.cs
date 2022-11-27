namespace Actonymous.Core.Module.Contracts;

using HotChocolate.Execution.Configuration;

public interface IGraphQlModuleSettings
{
    void RegisterModule(IRequestExecutorBuilder graphQlBuilder);
}