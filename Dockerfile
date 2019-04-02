FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Outreach.Reporting.Api/Outreach.Reporting.Api.csproj", "Outreach.Reporting.Api/"]
COPY ["Outreach.Reporting.Entity/Outreach.Reporting.Entity.csproj", "Outreach.Reporting.Entity/"]
COPY ["Outreach.Reporting.Business/Outreach.Reporting.Business.csproj", "Outreach.Reporting.Business/"]
COPY ["Outreach.Reporting.Data/Outreach.Reporting.Data.csproj", "Outreach.Reporting.Data/"]
RUN dotnet restore "Outreach.Reporting.Api/Outreach.Reporting.Api.csproj"
COPY . .
WORKDIR "/src/Outreach.Reporting.Api"
RUN dotnet build "Outreach.Reporting.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Outreach.Reporting.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Outreach.Reporting.Api.dll"]