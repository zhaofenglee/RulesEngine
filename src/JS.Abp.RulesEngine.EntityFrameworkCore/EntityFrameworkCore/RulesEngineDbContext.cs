using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.RulesEngine.EntityFrameworkCore;

[ConnectionStringName(RulesEngineDbProperties.ConnectionStringName)]
public class RulesEngineDbContext : AbpDbContext<RulesEngineDbContext>, IRulesEngineDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public RulesEngineDbContext(DbContextOptions<RulesEngineDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureRulesEngine();
    }
}
