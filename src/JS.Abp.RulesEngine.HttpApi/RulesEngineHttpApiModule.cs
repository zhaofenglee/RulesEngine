using Localization.Resources.AbpUi;
using JS.Abp.RulesEngine.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(RulesEngineApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class RulesEngineHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(RulesEngineHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<RulesEngineResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
