namespace Actonymous.API.ReportSettingsExporter.Services;

using Actonymous.API.ReportSettingsExporter.DAL;

using DocsReportSettingsExporter.V1;

using Google.Protobuf;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

[PublicAPI]
public sealed class ExportReportSettingsService : IReportSettingsExporterDataService
{
    private readonly AppDbContext _dbContext;

    private readonly ReportSettingsDto _emptyReportSettings;

    private readonly Mapper _mapper;

    public ExportReportSettingsService(AppDbContext dbContext, Mapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _emptyReportSettings = GetEmptyReportSettings();
    }

    public async Task<ReportSettingsDto> GetAsync() //TODO: change on this signature when user was added (BaseRecordDto dto)
    {
        //TODO: fix when user was added
        // var foundEntity = await _dbContext.FindAsync<ExportReportSettings>(dto.Id).ConfigureAwait(false);
        var foundEntity = await _dbContext.ExportReportSettings.SingleOrDefaultAsync().ConfigureAwait(false);
        if (foundEntity is null)
            return _emptyReportSettings;

        var foundRecord = _mapper.MapFromEntityExportReportSettings(foundEntity);

        return foundRecord;
    }

    public async Task<SavedReportSettingsDto> SaveAsync(SavingReportSettingsDto data,
        CancellationToken cancellationToken = default)
    {
        var entity = _mapper.MapToEntityExportReportSettings(data);
        await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new SavedReportSettingsDto
        {
            Id = (ulong) entity.Id
        };
    }

    private static ReportSettingsDto GetEmptyReportSettings()
    {
        return new ReportSettingsDto
        {
            Id = 0,
            JiraCredentials = new JiraCredentialsDto
            {
                Login = string.Empty,
                Password = string.Empty,
                ServerAddress = string.Empty
            },
            TemplateSettings = new TemplateSettingsDto
            {
                CustomerInfo = new CustomerInfoDto
                {
                    CompanyName = string.Empty,
                    HeaderFullname = string.Empty,
                    HeaderPosition = string.Empty
                },
                ExecutorInfo = new ExecutorInfoDto
                {
                    CompanyName = string.Empty,
                    HeaderFullname = string.Empty,
                    HeaderPosition = string.Empty,
                    RatePerHour = 0
                },
                ContractInfo = new ContractInfoDto
                {
                    ContractNumber = string.Empty,
                    ApprovalDate = default,
                    ContractFile = ByteString.Empty
                },
                TemplateFilesInfo = new TemplateFilesInfoDto
                {
                    ActTemplateFile = ByteString.Empty,
                    TaskTemplateFile = ByteString.Empty
                }
            },
            MorpherSettings = new MorpherSettingsDto
            {
                AccessToken = string.Empty
            }
        };
    }
}