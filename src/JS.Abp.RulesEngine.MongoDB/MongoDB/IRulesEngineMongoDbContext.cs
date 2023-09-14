using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace JS.Abp.RulesEngine.MongoDB;

[ConnectionStringName(RulesEngineDbProperties.ConnectionStringName)]
public interface IRulesEngineMongoDbContext : IAbpMongoDbContext
{
    IMongoCollection<RulesMember> RulesMembers { get; }
    IMongoCollection<RulesGroup> RulesGroups { get; }
    IMongoCollection<Rule> Rules { get; }
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}