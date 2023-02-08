FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Actonymous.API.DocsPackagerStorage/Actonymous.API.DocsPackagerStorage.csproj", "Actonymous.API.DocsPackagerStorage/"]
COPY ["Actonymous.Core.Protos/Itadakimasu.Core.Protos.csproj", "Actonymous.Core.Protos/"]
RUN dotnet restore "Actonymous.API.DocsPackagerStorage/Actonymous.API.DocsPackagerStorage.csproj"
COPY . .
WORKDIR "/src/Actonymous.API.DocsPackagerStorage"
RUN dotnet build "Actonymous.API.DocsPackagerStorage.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Actonymous.API.DocsPackagerStorage.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Actonymous.API.DocsPackagerStorage.dll"]
