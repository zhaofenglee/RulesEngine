using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace JS.Abp.RulesEngine.EntityFrameworkCore;

public class RulesEngineHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<RulesEngineHttpApiHostMigrationsDbContext>
{
    public RulesEngineHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<RulesEngineHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("RulesEngine"));

        return new RulesEngineHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
