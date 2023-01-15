# Add a new migration

Change the current directory to migration's directory:
> cd .\Actonymous.API.ReportSettingsExporter.Migrations\

To add a new migration just use the next command:
> dotnet ef migrations add InitEntitiesSettings --context AppDbContext --startup-project ..\Actonymous.API.ReportSettingsExporter
> 
> dotnet ef database update --context AppDbContext --startup-project ..\Actonymous.API.ReportSettingsExporter
>
> dotnet ef migrations remove --context AppDbContext --startup-project ..\Actonymous.API.ReportSettingsExporter