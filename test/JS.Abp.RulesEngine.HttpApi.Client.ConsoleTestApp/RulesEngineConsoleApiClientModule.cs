using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(RulesEngineHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class RulesEngineConsoleApiClientModule : AbpModule
{

}
