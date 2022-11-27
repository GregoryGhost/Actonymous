FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Itadakimasu.Core.Protos/Itadakimasu.Core.Protos.csproj", "Itadakimasu.Core.Protos/"]
COPY ["Actonymous.API.Gateway.Settings.ExportReport/Actonymous.API.Gateway.Settings.ExportReport.csproj", "Actonymous.API.Gateway.Settings.ExportReport/"]
COPY ["Actonymous.API.Gateway.Settings.ExportReport.DAL/Actonymous.API.Gateway.Settings.ExportReport.DAL.csproj", "Actonymous.API.Gateway.Settings.ExportReport.DAL/"]
COPY ["Actonymous.API.Gateway.Settings.ExportReport.Domain/Actonymous.API.Gateway.Settings.ExportReport.Domain.csproj", "Actonymous.API.Gateway.Settings.ExportReport.Domain/"]
COPY ["Actonymous.API.Gateway/Actonymous.API.Gateway.csproj", "Actonymous.API.Gateway/"]
RUN dotnet restore "Actonymous.API.Gateway/Actonymous.API.Gateway.csproj"
COPY . .
WORKDIR "/src/Actonymous.API.Gateway"
RUN dotnet build "Actonymous.API.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Actonymous.API.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Actonymous.API.Gateway.dll"]
