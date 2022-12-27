namespace Actonymous.Core.Module.Extensions;

using Grpc.Net.ClientFactory;

public static class StartupDiHelper
{
    public static void SetClientOptions(string environmentVariable, GrpcClientFactoryOptions options)
    {
        var apiAddress = Environment.GetEnvironmentVariable(environmentVariable);
        if (string.IsNullOrWhiteSpace(apiAddress))
            throw new Exception("You must provide correct address.");

        options.Address = new Uri(apiAddress);
    }
}