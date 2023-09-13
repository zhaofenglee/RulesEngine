using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(RulesEngineDomainModule),
    typeof(RulesEngineApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class RulesEngineApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<RulesEngineApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<RulesEngineApplicationModule>(validate: true);
        });
    }
}
