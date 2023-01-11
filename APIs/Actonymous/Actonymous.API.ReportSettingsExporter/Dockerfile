FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Actonymous.API.ReportSettingsExporter/Actonymous.API.ReportSettingsExporter.csproj", "Actonymous.API.ReportSettingsExporter/"]
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
