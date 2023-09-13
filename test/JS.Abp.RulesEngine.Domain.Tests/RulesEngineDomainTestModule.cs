using JS.Abp.RulesEngine.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(RulesEngineEntityFrameworkCoreTestModule)
    )]
public class RulesEngineDomainTestModule : AbpModule
{

}
