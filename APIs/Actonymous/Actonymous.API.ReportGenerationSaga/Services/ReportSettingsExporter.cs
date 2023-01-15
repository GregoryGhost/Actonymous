namespace Actonymous.API.ReportGenerationSaga.Services;

//TODO: remove the project reference from this project
using System;
using System.Threading.Tasks;

using Actonymous.API.ReportSettingsExporter.Domain.DTOs;

using JetBrains.Annotations;

using MassTransit;

[UsedImplicitly]
public sealed class ReportSettingsExporter : IReportSettingsExporter
{
    private readonly IBus _bus;

    private readonly ExportedReportSettingsDto _exportedReportSettings;

    public ReportSettingsExporter(IBus bus)
    {
        _bus = bus;
        _exportedReportSettings = GetFakeReportSettings();
    }

    private static ExportedReportSettingsDto GetFakeReportSettings()
    {
        throw new NotImplementedException("TODO: need to take a real data from ReportSettingsExporter API microservice.");
    }

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<ExportingReportSettingsDto> context)
    {
        var data = GetSettings();

        await context.RespondAsync<ExportedReportSettingsDto>(data);

        //TODO: use when gRPC service will be ready
        // using var call = _reportSettingsExporter.GetSettings();

        // await WriteAsync(call, context.Message);
        // await ReadAsync(call);
        //     
        // await call.RequestStream.CompleteAsync();
    }

    public ExportedReportSettingsDto GetSettings()
    {
        return _exportedReportSettings;
    }
}

public interface IReportSettingsExporter: IConsumer<ExportingReportSettingsDto>
{
    ExportedReportSettingsDto GetSettings();
}

public record ExportedReportSettingsDto
{
    public JiraCredentialsDto JiraCredentials { get; set; } = null!;

    public TemplateSettingsDto TemplateSettings { get; set; } = null!;

    public MorpherSettingsDto MorpherSettings { get; set; } = null!;
}

public record ExportingReportSettingsDto
{
}