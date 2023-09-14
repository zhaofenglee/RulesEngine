using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.RulesEngine.EntityFrameworkCore;

[ConnectionStringName(RulesEngineDbProperties.ConnectionStringName)]
public class RulesEngineDbContext : AbpDbContext<RulesEngineDbContext>, IRulesEngineDbContext
{
    public DbSet<RulesMember> RulesMembers { get; set; }
    public DbSet<RulesGroup> RulesGroups { get; set; }
    public DbSet<Rule> Rules { get; set; }
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