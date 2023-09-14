using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace JS.Abp.RulesEngine.MongoDB;

[ConnectionStringName(RulesEngineDbProperties.ConnectionStringName)]
public class RulesEngineMongoDbContext : AbpMongoDbContext, IRulesEngineMongoDbContext
{
    public IMongoCollection<RulesMember> RulesMembers => Collection<RulesMember>();
    public IMongoCollection<RulesGroup> RulesGroups => Collection<RulesGroup>();
    public IMongoCollection<Rule> Rules => Collection<Rule>();
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureRulesEngine();

        modelBuilder.Entity<Rule>(b => { b.CollectionName = RulesEngineDbProperties.DbTablePrefix + "Rules"; });

        modelBuilder.Entity<RulesGroup>(b => { b.CollectionName = RulesEngineDbProperties.DbTablePrefix + "RulesGroups"; });

        modelBuilder.Entity<RulesMember>(b => { b.CollectionName = RulesEngineDbProperties.DbTablePrefix + "RulesMembers"; });
    }
}