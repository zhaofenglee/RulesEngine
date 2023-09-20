#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["host/JS.Abp.RulesEngine.Web.Host/JS.Abp.RulesEngine.Web.Host.csproj", "host/JS.Abp.RulesEngine.Web.Host/"]
COPY ["src/JS.Abp.RulesEngine.Web/JS.Abp.RulesEngine.Web.csproj", "src/JS.Abp.RulesEngine.Web/"]
COPY ["src/JS.Abp.RulesEngine.Application.Contracts/JS.Abp.RulesEngine.Application.Contracts.csproj", "src/JS.Abp.RulesEngine.Application.Contracts/"]
COPY ["src/JS.Abp.RulesEngine.Domain.Shared/JS.Abp.RulesEngine.Domain.Shared.csproj", "src/JS.Abp.RulesEngine.Domain.Shared/"]
COPY ["src/JS.Abp.RulesEngine.HttpApi.Client/JS.Abp.RulesEngine.HttpApi.Client.csproj", "src/JS.Abp.RulesEngine.HttpApi.Client/"]
COPY ["src/JS.Abp.RulesEngine.HttpApi/JS.Abp.RulesEngine.HttpApi.csproj", "src/JS.Abp.RulesEngine.HttpApi/"]
COPY ["host/JS.Abp.RulesEngine.Host.Shared/JS.Abp.RulesEngine.Host.Shared.csproj", "host/JS.Abp.RulesEngine.Host.Shared/"]
RUN dotnet restore "host/JS.Abp.RulesEngine.Web.Host/JS.Abp.RulesEngine.Web.Host.csproj"
COPY . .
WORKDIR "/src/host/JS.Abp.RulesEngine.Web.Host"
RUN dotnet build "JS.Abp.RulesEngine.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JS.Abp.RulesEngine.Web.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JS.Abp.RulesEngine.Web.Host.dll"]