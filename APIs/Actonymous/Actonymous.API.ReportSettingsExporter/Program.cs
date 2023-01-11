using Actonymous.API.ReportSettingsExporter.DAL;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
    throw new ApplicationException("You should provide connection string to database.");
builder.Services.AddDbContext<AppDbContext>(
    optionsBuilder => optionsBuilder.UseNpgsql(
        connectionString,
        b => b.MigrationsAssembly(nameof(Actonymous.API.ReportSettingsExporter.Migrations))));
app.Run();