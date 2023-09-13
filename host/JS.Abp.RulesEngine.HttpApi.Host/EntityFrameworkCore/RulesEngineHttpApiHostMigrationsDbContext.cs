using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.RulesEngine.EntityFrameworkCore;

public class RulesEngineHttpApiHostMigrationsDbContext : AbpDbContext<RulesEngineHttpApiHostMigrationsDbContext>
{
    public RulesEngineHttpApiHostMigrationsDbContext(DbContextOptions<RulesEngineHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureRulesEngine();
    }
}
