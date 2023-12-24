using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine.Blazor.Server;

[DependsOn(
    typeof(RulesEngineBlazorModule),
    typeof(AbpAspNetCoreComponentsServerThemingModule)
    )]
public class RulesEngineBlazorServerModule : AbpModule
{

}
