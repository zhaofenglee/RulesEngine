using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(RulesEngineDomainSharedModule)
)]
public class RulesEngineDomainModule : AbpModule
{

}
