using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(RulesEngineDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class RulesEngineApplicationContractsModule : AbpModule
{

}
