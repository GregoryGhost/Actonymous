namespace Actonymous.API.Gateway.Report.ReportGeneration.APIs;

using Actonymous.API.Gateway.Report.ReportGeneration.DTOs;
using Actonymous.API.ReportGenerationSaga.PublicDTOs.DTOs;

using HotChocolate;
using HotChocolate.Types;

using JetBrains.Annotations;

using MassTransit;

[PublicAPI]
[ExtendObjectType("Mutation")]
public class Mutation
{
    public async Task GenerateReportRequest(ReportGenerationInfoDto reportGenerationInfo,
        [Service] IPublishEndpoint publishEndpoint, CancellationToken cancellationToken)
    {
        var reportGenerationRequest = new ReportGenerationRequest
        {
            From = reportGenerationInfo.From,
            To = reportGenerationInfo.To
        };
        await publishEndpoint.Publish(reportGenerationRequest, cancellationToken);
    }
}