using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.RulesEngine.EntityFrameworkCore;

[ConnectionStringName(RulesEngineDbProperties.ConnectionStringName)]
public interface IRulesEngineDbContext : IEfCoreDbContext
{
    DbSet<RulesMember> RulesMembers { get; set; }
    DbSet<RulesGroup> RulesGroups { get; set; }
    DbSet<Rule> Rules { get; set; }
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}