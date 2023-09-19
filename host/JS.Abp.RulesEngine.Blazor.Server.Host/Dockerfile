#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["host/JS.Abp.RulesEngine.Blazor.Server.Host/JS.Abp.RulesEngine.Blazor.Server.Host.csproj", "host/JS.Abp.RulesEngine.Blazor.Server.Host/"]
COPY ["src/JS.Abp.RulesEngine.Application/JS.Abp.RulesEngine.Application.csproj", "src/JS.Abp.RulesEngine.Application/"]
COPY ["src/JS.Abp.RulesEngine.Application.Contracts/JS.Abp.RulesEngine.Application.Contracts.csproj", "src/JS.Abp.RulesEngine.Application.Contracts/"]
COPY ["src/JS.Abp.RulesEngine.Domain.Shared/JS.Abp.RulesEngine.Domain.Shared.csproj", "src/JS.Abp.RulesEngine.Domain.Shared/"]
COPY ["src/JS.Abp.RulesEngine.Domain/JS.Abp.RulesEngine.Domain.csproj", "src/JS.Abp.RulesEngine.Domain/"]
COPY ["src/JS.Abp.RulesEngine.Blazor.Server/JS.Abp.RulesEngine.Blazor.Server.csproj", "src/JS.Abp.RulesEngine.Blazor.Server/"]
COPY ["src/JS.Abp.RulesEngine.Blazor/JS.Abp.RulesEngine.Blazor.csproj", "src/JS.Abp.RulesEngine.Blazor/"]
COPY ["src/JS.Abp.RulesEngine.EntityFrameworkCore/JS.Abp.RulesEngine.EntityFrameworkCore.csproj", "src/JS.Abp.RulesEngine.EntityFrameworkCore/"]
COPY ["src/JS.Abp.RulesEngine.HttpApi/JS.Abp.RulesEngine.HttpApi.csproj", "src/JS.Abp.RulesEngine.HttpApi/"]
COPY ["host/JS.Abp.RulesEngine.Host.Shared/JS.Abp.RulesEngine.Host.Shared.csproj", "host/JS.Abp.RulesEngine.Host.Shared/"]
RUN dotnet restore "host/JS.Abp.RulesEngine.Blazor.Server.Host/JS.Abp.RulesEngine.Blazor.Server.Host.csproj"
COPY . .
WORKDIR "/src/host/JS.Abp.RulesEngine.Blazor.Server.Host"
RUN dotnet build "JS.Abp.RulesEngine.Blazor.Server.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JS.Abp.RulesEngine.Blazor.Server.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JS.Abp.RulesEngine.Blazor.Server.Host.dll"]