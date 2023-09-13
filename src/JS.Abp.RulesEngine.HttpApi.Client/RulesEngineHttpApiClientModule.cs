using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(RulesEngineApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class RulesEngineHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(RulesEngineApplicationContractsModule).Assembly,
            RulesEngineRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<RulesEngineHttpApiClientModule>();
        });

    }
}
