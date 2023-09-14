using JS.Abp.RulesEngine.Permissions;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using JS.Abp.RulesEngine.Localization;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.RulesEngine.Blazor.Menus;

public class RulesEngineMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
            var moduleMenu = AddModuleMenuItem(context);
            AddMenuItemRulesGroups(context, moduleMenu);
            //AddMenuItemRulesMembers(context, moduleMenu);
            AddMenuItemRules(context, moduleMenu);
        }

    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        var l = context.GetLocalizer<RulesEngineResource>();

        // context.Menu.AddItem(new ApplicationMenuItem(
        //     RulesEngineMenus.Prefix,
        //     displayName: l["Menu:RulesEngine"],
        //     "/RulesEngine",
        //     icon: "fa fa-globe"));

        await Task.CompletedTask;
    }
    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var moduleMenu = new ApplicationMenuItem(
            RulesEngineMenus.Prefix,
            context.GetLocalizer<RulesEngineResource>()["Menu:RulesEngine"],
            icon: "fa fa-folder"
        );

        context.Menu.Items.AddIfNotContains(moduleMenu);
        return moduleMenu;
    }
    private static void AddMenuItemRules(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.RulesEngineMenus.Rules,
                context.GetLocalizer<RulesEngineResource>()["Menu:Rules"],
                "/RulesEngine/Rules",
                icon: "fa fa-file-alt",
                requiredPermissionName: RulesEnginePermissions.Rules.Default
            )
        );
    }

    private static void AddMenuItemRulesGroups(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.RulesEngineMenus.RulesGroups,
                context.GetLocalizer<RulesEngineResource>()["Menu:RulesGroups"],
                "/RulesEngine/RulesGroups",
                icon: "fa fa-file-alt",
                requiredPermissionName: RulesEnginePermissions.RulesGroups.Default
            )
        );
    }

    private static void AddMenuItemRulesMembers(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
    {
        parentMenu.AddItem(
            new ApplicationMenuItem(
                Menus.RulesEngineMenus.RulesMembers,
                context.GetLocalizer<RulesEngineResource>()["Menu:RulesMembers"],
                "/RulesEngine/RulesMembers",
                icon: "fa fa-file-alt",
                requiredPermissionName: RulesEnginePermissions.RulesMembers.Default
            )
        );
    }
}