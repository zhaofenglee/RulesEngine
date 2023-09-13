using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace JS.Abp.RulesEngine.EntityFrameworkCore;

[ConnectionStringName(RulesEngineDbProperties.ConnectionStringName)]
public interface IRulesEngineDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
