FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
COPY . .

WORKDIR /templates/service/host/AuthServerHost
RUN dotnet restore
RUN dotnet ef migrations script -i -o migrations-AuthServerHost.sql

WORKDIR /templates/service/host/JS.Abp.RulesEngine.Host
RUN dotnet restore
RUN dotnet ef migrations script -i -o migrations-RulesEngine.sql

FROM mcr.microsoft.com/mssql-tools AS final
WORKDIR /src
COPY --from=build /templates/service/host/AuthServerHost/migrations-AuthServerHost.sql migrations-AuthServerHost.sql
COPY --from=build /templates/service/host/JS.Abp.RulesEngine.Host/migrations-RulesEngine.sql migrations-RulesEngine.sql
COPY --from=build /templates/service/database/entrypoint.sh .
RUN /bin/bash -c "sed -i $'s/\r$//' entrypoint.sh"
RUN chmod +x ./entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]