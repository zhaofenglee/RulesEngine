using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using JS.Abp.RulesEngine.Localization;
using JS.Abp.RulesEngine.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using JS.Abp.RulesEngine.Permissions;

namespace JS.Abp.RulesEngine.Web;

[DependsOn(
    typeof(RulesEngineApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class RulesEngineWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(RulesEngineResource), typeof(RulesEngineWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(RulesEngineWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new RulesEngineMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<RulesEngineWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<RulesEngineWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<RulesEngineWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
            //Configure authorization.
            options.Conventions.AuthorizePage("/Rules/Index", RulesEnginePermissions.Rules.Default);
            options.Conventions.AuthorizePage("/RulesGroups/Index", RulesEnginePermissions.RulesGroups.Default);
            options.Conventions.AuthorizePage("/RulesMembers/Index", RulesEnginePermissions.RulesMembers.Default);
        });
    }
}