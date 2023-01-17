namespace Actonymous.API.ReportSettingsExporter.Services;

using Actonymous.API.ReportSettingsExporter.DAL.Entities;

using DocsReportSettingsExporter.V1;

using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;

public class Mapper
{
    public ExportReportSettings MapToEntityExportReportSettings(SavingReportSettingsDto dto)
    {
        var jiraCredentials = GetJiraCredentialsEntity(dto.JiraCredentials);
        var templateSettings = GetTemplateSettingsEntity(dto.TemplateSettings);
        var mopherSettings = GetMopherSettingsEntity(dto.MorpherSettings);
        var settingsId = (long)dto.Id;
        
        return new ExportReportSettings
        {
            Id = settingsId,
            JiraCredentials = jiraCredentials,
            TemplateSettings = templateSettings,
            MorpherSettings = mopherSettings
        };
    }

    private static MorpherSettings GetMopherSettingsEntity(MorpherSettingsDto morpherSettingsDto)
    {
        return new MorpherSettings
        {
            Id = 0,
            AccessToken = morpherSettingsDto.AccessToken
        };
    }

    private static TemplateSettings GetTemplateSettingsEntity(TemplateSettingsDto templateSettingsDto)
    {
        var customerInfo = GetCustomerInfoEntity(templateSettingsDto.CustomerInfo);
        var executorInfo = GetExecutorInfoEntity(templateSettingsDto.ExecutorInfo);
        var contractInfo = GetContractInfoEntity(templateSettingsDto.ContractInfo);
        var templateFilesInfo = GetTemplateFilesInfoEntity(templateSettingsDto.TemplateFilesInfo);

        var templateSettings = new TemplateSettings
        {
            Id = 0,
            CustomerInfo = customerInfo,
            ExecutorInfo = executorInfo,
            ContractInfo = contractInfo,
            TemplateFilesInfo = templateFilesInfo
        };
        
        return templateSettings;
    }

    private static TemplateFilesInfo GetTemplateFilesInfoEntity(TemplateFilesInfoDto templateFilesInfoDto)
    {
        var actTemplateFile = templateFilesInfoDto.ActTemplateFile.ToByteArray();
        var taskTemplateFile = templateFilesInfoDto.TaskTemplateFile.ToByteArray();
        
        return new TemplateFilesInfo
        {
            Id = 0,
            ActTemplateFile = actTemplateFile,
            TaskTemplateFile = taskTemplateFile
        };
    }

    private static ContractInfo GetContractInfoEntity(ContractInfoDto contractInfoDto)
    {
        var approvalDate = contractInfoDto.ApprovalDate.ToDateTime();
        var contractFile = contractInfoDto.ContractFile.ToByteArray();
        
        return new ContractInfo
        {
            Id = 0,
            ContractNumber = contractInfoDto.ContractNumber,
            ApprovalDate = approvalDate,
            ContractFile = contractFile
        };
    }

    private static ExecutorInfo GetExecutorInfoEntity(ExecutorInfoDto executorInfoDto)
    {
        return new ExecutorInfo
        {
            Id = 0,
            CompanyName = executorInfoDto.CompanyName,
            HeaderFullname = executorInfoDto.HeaderFullname,
            HeaderPosition = executorInfoDto.HeaderPosition,
            RatePerHour = executorInfoDto.RatePerHour
        };
    }

    private static CustomerInfo GetCustomerInfoEntity(CustomerInfoDto customerInfoDto)
    {
        return new CustomerInfo
        {
            Id = 0,
            CompanyName = customerInfoDto.CompanyName,
            HeaderFullname = customerInfoDto.HeaderFullname,
            HeaderPosition = customerInfoDto.HeaderPosition
        };
    }

    private static JiraCredentials GetJiraCredentialsEntity(JiraCredentialsDto jiraCredentialsDto)
    {
        return new JiraCredentials
        {
            Id = 0,
            Login = jiraCredentialsDto.Login,
            Password = jiraCredentialsDto.Password,
            ServerAddress = jiraCredentialsDto.ServerAddress
        };
    }

    public ReportSettingsDto MapFromEntityExportReportSettings(ExportReportSettings entity)
    {
        var jiraCredentials = GetJiraCredentialsDto(entity.JiraCredentials);
        var templateSettings = GetTemplateSettingsDto(entity.TemplateSettings);
        var mopherSettings = GetMopherSettingsDto(entity.MorpherSettings);
        var settingsId = (ulong)entity.Id;
        
        return new ReportSettingsDto
        {
            Id = settingsId,
            JiraCredentials = jiraCredentials,
            TemplateSettings = templateSettings,
            MorpherSettings = mopherSettings
        };
    }

    private static MorpherSettingsDto GetMopherSettingsDto(MorpherSettings morpherSettings)
    {
        return new MorpherSettingsDto
        {
            AccessToken = morpherSettings.AccessToken
        };
    }

    private static TemplateSettingsDto GetTemplateSettingsDto(TemplateSettings templateSettings)
    {
        var customerInfoDto = GetCustomerInfoDto(templateSettings.CustomerInfo);
        var executorInfoDto = GetExecutorInfoDto(templateSettings.ExecutorInfo);
        var contractInfoDto = GetContractInfoDto(templateSettings.ContractInfo);
        var templateFilesInfoDto = GetTemplateFilesInfoDto(templateSettings.TemplateFilesInfo);

        var templateSettingsDto = new TemplateSettingsDto
        {
            CustomerInfo = customerInfoDto,
            ExecutorInfo = executorInfoDto,
            ContractInfo = contractInfoDto,
            TemplateFilesInfo = templateFilesInfoDto
        };

        return templateSettingsDto;
    }

    private static TemplateFilesInfoDto GetTemplateFilesInfoDto(TemplateFilesInfo templateFilesInfo)
    {
        var actTemplateFile = ByteString.CopyFrom(templateFilesInfo.ActTemplateFile);
        var taskTemplateFile = ByteString.CopyFrom(templateFilesInfo.TaskTemplateFile);
        
        return new TemplateFilesInfoDto
        {
            ActTemplateFile = actTemplateFile,
            TaskTemplateFile = taskTemplateFile
        };
    }

    private static ContractInfoDto GetContractInfoDto(ContractInfo contractInfoDto)
    {
        var approvalDate = contractInfoDto.ApprovalDate.ToTimestamp();
        var contractFile = ByteString.CopyFrom(contractInfoDto.ContractFile);
        
        return new ContractInfoDto
        {
            ContractNumber = contractInfoDto.ContractNumber,
            ApprovalDate = approvalDate,
            ContractFile = contractFile
        };
    }

    private static ExecutorInfoDto GetExecutorInfoDto(ExecutorInfo executorInfoDto)
    {
        return new ExecutorInfoDto
        {
            CompanyName = executorInfoDto.CompanyName,
            HeaderFullname = executorInfoDto.HeaderFullname,
            HeaderPosition = executorInfoDto.HeaderPosition,
            RatePerHour = executorInfoDto.RatePerHour
        };
    }

    private static CustomerInfoDto GetCustomerInfoDto(CustomerInfo customerInfo)
    {
        return new CustomerInfoDto
        {
            CompanyName = customerInfo.CompanyName,
            HeaderFullname = customerInfo.HeaderFullname,
            HeaderPosition = customerInfo.HeaderPosition
        };
    }

    private static JiraCredentialsDto GetJiraCredentialsDto(JiraCredentials jiraCredentials)
    {
        return new JiraCredentialsDto
        {
            Login = jiraCredentials.Login,
            Password = jiraCredentials.Password,
            ServerAddress = jiraCredentials.ServerAddress
        };
    }
}