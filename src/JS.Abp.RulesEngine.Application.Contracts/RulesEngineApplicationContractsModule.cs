using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(RulesEngineDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationAbstractionsModule)
    )]
public class RulesEngineApplicationContractsModule : AbpModule
{

}
