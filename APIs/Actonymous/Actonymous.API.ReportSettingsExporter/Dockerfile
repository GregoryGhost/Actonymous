FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Actonymous.API.ReportSettingsExporter/Actonymous.API.ReportSettingsExporter.csproj", "Actonymous.API.ReportSettingsExporter/"]
COPY ["Actonymous.API.ReportSettingsExporter.DAL/Actonymous.API.ReportSettingsExporter.DAL.csproj", "Actonymous.API.ReportSettingsExporter.DAL/"]
COPY ["Actonymous.API.ReportSettingsExporter.Migrations/Actonymous.API.ReportSettingsExporter.Migrations.csproj", "Actonymous.API.ReportSettingsExporter.Migrations/"]
COPY ["Actonymous.Core.Protos/Itadakimasu.Core.Protos.csproj", "Actonymous.Core.Protos/"]
RUN dotnet restore "Actonymous.API.ReportSettingsExporter/Actonymous.API.ReportSettingsExporter.csproj"
COPY . .
WORKDIR "/src/Actonymous.API.ReportSettingsExporter"
RUN dotnet build "Actonymous.API.ReportSettingsExporter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Actonymous.API.ReportSettingsExporter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Actonymous.API.ReportSettingsExporter.dll"]
