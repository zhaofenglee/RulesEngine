using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace JS.Abp.RulesEngine.MongoDB;

[DependsOn(
    typeof(RulesEngineTestBaseModule),
    typeof(RulesEngineMongoDbModule)
    )]
public class RulesEngineMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
