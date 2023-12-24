using JS.Abp.RulesEngine.MongoDB;
using JS.Abp.RulesEngine.Stores;
using Xunit;

namespace JS.Abp.RulesEngine.EntityFrameworkCore.Domain;

[Collection(MongoTestCollection.Name)]
public class MongoDbRulesEngineStoreDomainTests : RulesEngineStoreTests<RulesEngineMongoDbTestModule>
{
    
}