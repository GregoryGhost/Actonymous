FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Actonymous.API.Tex2PdfRenderer/Actonymous.API.Tex2PdfRenderer.csproj", "Actonymous.API.Tex2PdfRenderer/"]
COPY ["Actonymous.Core.Protos/Itadakimasu.Core.Protos.csproj", "Actonymous.Core.Protos/"]
RUN dotnet restore "Actonymous.API.Tex2PdfRenderer/Actonymous.API.Tex2PdfRenderer.csproj"
COPY . .
WORKDIR "/src/Actonymous.API.Tex2PdfRenderer"
RUN dotnet build "Actonymous.API.Tex2PdfRenderer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Actonymous.API.Tex2PdfRenderer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Actonymous.API.Tex2PdfRenderer.dll"]
