namespace Actonymous.API.ReportGenerationSaga.Services;

//TODO: remove the project reference from this project
using Actonymous.API.Gateway.Settings.ExportReport.Domain.DTOs;

using JetBrains.Annotations;

using MassTransit;

[UsedImplicitly]
public sealed class ReportSettingsExporter : IReportSettingsExporter
{
    private readonly IBus _bus;

    private readonly ExportReportSettingsDto _exportedReportSettings;

    public ReportSettingsExporter(IBus bus)
    {
        _bus = bus;
        _exportedReportSettings = GetFakeReportSettings();
    }

    private static ExportReportSettingsDto GetFakeReportSettings()
    {
        throw new NotImplementedException("TODO: need to take a real data from ReportSettingsExporter API microservice.");
    }

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<ExportingReportSettingsRequest> context)
    {
        var data = GetSettings();

        await context.RespondAsync<ExportedReportSettingsResponse>(data);

        //TODO: use when gRPC service will be ready
        // using var call = _reportSettingsExporter.GetSettings();

        // await WriteAsync(call, context.Message);
        // await ReadAsync(call);
        //     
        // await call.RequestStream.CompleteAsync();
    }

    public ExportReportSettingsDto GetSettings()
    {
        return _exportedReportSettings;
    }
}

public record ExportedReportSettingsResponse : ExportReportSettingsDto
{
}

public interface IReportSettingsExporter: IConsumer<ExportingReportSettingsRequest>
{
    ExportReportSettingsDto GetSettings();
}

public record ExportReportSettingsDto
{
    public JiraCredentialsDto JiraCredentials { get; set; } = null!;

    public TemplateSettingsDto TemplateSettings { get; set; } = null!;

    public MorpherSettingsDto MorpherSettings { get; set; } = null!;
}

public record ExportingReportSettingsRequest
{
}