FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Actonymous.API.Gateway.ReportGenerationSaga/Actonymous.API.Gateway.ReportGenerationSaga.csproj", "Actonymous.API.Gateway.ReportGenerationSaga/"]
RUN dotnet restore "Actonymous.API.Gateway.ReportGenerationSaga/Actonymous.API.Gateway.ReportGenerationSaga.csproj"
COPY . .
WORKDIR "/src/Actonymous.API.Gateway.ReportGenerationSaga"
RUN dotnet build "Actonymous.API.Gateway.ReportGenerationSaga.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Actonymous.API.Gateway.ReportGenerationSaga.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Actonymous.API.Gateway.ReportGenerationSaga.dll"]
