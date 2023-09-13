using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(RulesEngineBlazorModule)
    )]
public class RulesEngineBlazorServerModule : AbpModule
{

}
