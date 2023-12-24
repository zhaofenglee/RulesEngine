using JS.Abp.RulesEngine.RulesMembers;
using Xunit;

namespace JS.Abp.RulesEngine.MongoDB.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDbRulesMembersAppServiceTests: RulesMembersAppServiceTests<RulesEngineMongoDbTestModule>
{
    
}