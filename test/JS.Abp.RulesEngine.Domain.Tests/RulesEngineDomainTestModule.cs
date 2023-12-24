using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(RulesEngineDomainModule),
    typeof(RulesEngineTestBaseModule)
)]
public class RulesEngineDomainTestModule : AbpModule
{

}
