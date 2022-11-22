namespace Actonymous.API.Gateway.Settings.ExportReport.DAL;

using Actonymous.API.Gateway.Settings.ExportReport.DAL.Entities;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

[PublicAPI]
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ExportReportSettings> ExportReportSettings { set; get; } = null!;

    public DbSet<JiraCredentials> JiraCredentials { set; get; } = null!;

    public DbSet<MorpherSettings> MorpherSettings { set; get; } = null!;

    public DbSet<TemplateSettings> TemplateSettings { set; get; } = null!;
}