using JS.Abp.RulesEngine.RulesGroups;
using Xunit;

namespace JS.Abp.RulesEngine.MongoDB.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDbRulesGroupsAppServiceTests: RulesGroupsAppServiceTests<RulesEngineMongoDbTestModule>
{
    
}