using Actonymous.API.ReportGenerationSaga;

using Microsoft.AspNetCore.Builder;

var app = WebApplication.CreateBuilder(args).RegisterServices().Build();

app.Run();