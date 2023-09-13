using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine.Blazor.WebAssembly;

[DependsOn(
    typeof(RulesEngineBlazorModule),
    typeof(RulesEngineHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class RulesEngineBlazorWebAssemblyModule : AbpModule
{

}
