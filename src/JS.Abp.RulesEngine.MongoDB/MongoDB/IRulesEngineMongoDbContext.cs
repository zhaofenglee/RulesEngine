using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace JS.Abp.RulesEngine.MongoDB;

[ConnectionStringName(RulesEngineDbProperties.ConnectionStringName)]
public interface IRulesEngineMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
