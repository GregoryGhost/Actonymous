namespace Actonymous.API.Gateway.Report.ReportGeneration.APIs;

using Actonymous.API.Gateway.Report.ReportGeneration.DTOs;

using HotChocolate;
using HotChocolate.Types;

using JetBrains.Annotations;

[PublicAPI]
[ExtendObjectType("Subscription")]
public class Subscription
{
    [Subscribe]
    public GeneratedReportDto NotifyReportGenerationCompleted(
        [EventMessage] GeneratedReportDto generatedReport) => generatedReport;
}