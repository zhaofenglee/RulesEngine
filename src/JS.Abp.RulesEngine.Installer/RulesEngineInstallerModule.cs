using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class RulesEngineInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<RulesEngineInstallerModule>();
        });
    }
}
