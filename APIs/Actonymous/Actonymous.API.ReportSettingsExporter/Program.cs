using Actonymous.API.ReportSettingsExporter.DAL;
using Actonymous.API.ReportSettingsExporter.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
    throw new ApplicationException("You should provide connection string to database.");
builder.Services.AddDbContext<AppDbContext>(
    optionsBuilder => optionsBuilder.UseNpgsql(
        connectionString,
        b => b.MigrationsAssembly(typeof(Actonymous.API.ReportSettingsExporter.Migrations.MigrationInit).Namespace)));

builder.Services.AddSingleton<Mapper>();
builder.Services.AddScoped<IReportSettingsExporterDataService, ExportReportSettingsService>();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();

app.Run();