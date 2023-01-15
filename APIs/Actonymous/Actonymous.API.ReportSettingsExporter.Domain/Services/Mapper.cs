namespace Actonymous.API.ReportSettingsExporter.Domain.Services;

using Actonymous.API.ReportSettingsExporter.DAL.Entities;
using Actonymous.API.ReportSettingsExporter.Domain.DTOs;

public class Mapper
{
    public ExportReportSettings MapToEntityExportReportSettings(ExportReportSettingsDto dto)
    {
        var jiraCredentials = GetJiraCredentialsEntity(dto.JiraCredentials);
        var templateSettings = GetTemplateSettingsEntity(dto.TemplateSettings);
        var mopherSettings = GetMopherSettingsEntity(dto.MorpherSettings);
        
        return new ExportReportSettings
        {
            Id = dto.Id,
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
        var customerInfo = GetCustomerInfoEntity(templateSettingsDto.CustomerInfoDto);
        var executorInfo = GetExecutorInfoEntity(templateSettingsDto.ExecutorInfoDto);
        var contractInfo = GetContractInfoEntity(templateSettingsDto.ContractInfoDto);
        var templateFilesInfo = GetTemplateFilesInfoEntity(templateSettingsDto.TemplateFilesInfoDto);

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
        return new TemplateFilesInfo
        {
            Id = 0,
            ActTemplateFile = templateFilesInfoDto.ActTemplateFile,
            TaskTemplateFile = templateFilesInfoDto.TaskTemplateFile
        };
    }

    private static ContractInfo GetContractInfoEntity(ContractInfoDto contractInfoDto)
    {
        return new ContractInfo
        {
            Id = 0,
            ContractNumber = contractInfoDto.ContractNumber,
            ApprovalDate = contractInfoDto.ApprovalDate,
            ContractFile = contractInfoDto.ContractFile
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

    public ExportReportSettingsDto MapFromEntityExportReportSettings(ExportReportSettings entity)
    {
        var jiraCredentials = GetJiraCredentialsDto(entity.JiraCredentials);
        var templateSettings = GetTemplateSettingsDto(entity.TemplateSettings);
        var mopherSettings = GetMopherSettingsDto(entity.MorpherSettings);
        
        return new ExportReportSettingsDto
        {
            Id = entity.Id,
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
            CustomerInfoDto = customerInfoDto,
            ExecutorInfoDto = executorInfoDto,
            ContractInfoDto = contractInfoDto,
            TemplateFilesInfoDto = templateFilesInfoDto
        };

        return templateSettingsDto;
    }

    private static TemplateFilesInfoDto GetTemplateFilesInfoDto(TemplateFilesInfo templateFilesInfoDto)
    {
        return new TemplateFilesInfoDto
        {
            ActTemplateFile = templateFilesInfoDto.ActTemplateFile,
            TaskTemplateFile = templateFilesInfoDto.TaskTemplateFile
        };
    }

    private static ContractInfoDto GetContractInfoDto(ContractInfo contractInfoDto)
    {
        return new ContractInfoDto
        {
            ContractNumber = contractInfoDto.ContractNumber,
            ApprovalDate = contractInfoDto.ApprovalDate,
            ContractFile = contractInfoDto.ContractFile
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