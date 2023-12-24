using JS.Abp.RulesEngine.Rules;
using Xunit;

namespace JS.Abp.RulesEngine.MongoDB.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDbRuleApplicationTests: RulesAppServiceTests<RulesEngineMongoDbTestModule>
{
    
}