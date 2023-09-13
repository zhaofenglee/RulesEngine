using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(RulesEngineApplicationModule),
    typeof(RulesEngineDomainTestModule)
    )]
public class RulesEngineApplicationTestModule : AbpModule
{

}
