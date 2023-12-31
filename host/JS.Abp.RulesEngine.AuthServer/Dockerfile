#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["host/JS.Abp.RulesEngine.AuthServer/JS.Abp.RulesEngine.AuthServer.csproj", "host/JS.Abp.RulesEngine.AuthServer/"]
COPY ["src/JS.Abp.RulesEngine.Application.Contracts/JS.Abp.RulesEngine.Application.Contracts.csproj", "src/JS.Abp.RulesEngine.Application.Contracts/"]
COPY ["src/JS.Abp.RulesEngine.Domain.Shared/JS.Abp.RulesEngine.Domain.Shared.csproj", "src/JS.Abp.RulesEngine.Domain.Shared/"]
COPY ["host/JS.Abp.RulesEngine.Host.Shared/JS.Abp.RulesEngine.Host.Shared.csproj", "host/JS.Abp.RulesEngine.Host.Shared/"]
RUN dotnet restore "host/JS.Abp.RulesEngine.AuthServer/JS.Abp.RulesEngine.AuthServer.csproj"
COPY . .
WORKDIR "/src/host/JS.Abp.RulesEngine.AuthServer"
RUN dotnet build "JS.Abp.RulesEngine.AuthServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JS.Abp.RulesEngine.AuthServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JS.Abp.RulesEngine.AuthServer.dll"]