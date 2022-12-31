namespace Actonymous.API.ReportGenerationSaga.Services;

using System.Threading.Tasks;

using Grpc.Core;

using JetBrains.Annotations;

using JiraWorklogManager.V1;

using MassTransit;

[UsedImplicitly]
public sealed class JiraWorklogManagerService: IConsumer<UserWorklogDto>
{
    private readonly JiraWorklogManager.JiraWorklogManagerClient _jiraWorklogManagerClient;
    
    private readonly IBus _bus;

    public JiraWorklogManagerService(JiraWorklogManager.JiraWorklogManagerClient jiraWorklogManagerClient, IBus bus)
    {
        _jiraWorklogManagerClient = jiraWorklogManagerClient;
        _bus = bus;
    }

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<UserWorklogDto> context)
    {
        using var call = _jiraWorklogManagerClient.GetUserWorklogs();
        
        await WriteAsync(call, context.Message);
        await ReadAsync(call);
            
        await call.RequestStream.CompleteAsync();
    }

    private async Task ReadAsync(AsyncDuplexStreamingCall<UserWorklogDto, UserWorklogInfoDto> call)
    {
        await foreach (var response in call.ResponseStream.ReadAllAsync())
        {
            await _bus.Publish(response);
        }
    }

    private static async Task WriteAsync(AsyncDuplexStreamingCall<UserWorklogDto, UserWorklogInfoDto> call,
        UserWorklogDto userWorkLog)
    {
        await call.RequestStream.WriteAsync(userWorkLog);
    }
}