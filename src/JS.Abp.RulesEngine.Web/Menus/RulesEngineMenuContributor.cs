using JS.Abp.RulesEngine.Permissions;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using JS.Abp.RulesEngine.Localization;
using Volo.Abp.Authorization.Permissions;

namespace JS.Abp.RulesEngine.Web.Menus;

public class RulesEngineMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name != StandardMenus.Main)
        {
            return;
        }

        var moduleMenu = AddModuleMenuItem(context); 

        AddMenuItemRulesGroups(context, moduleMenu);
        
        
        AddMenuItemRules(context, moduleMenu);

       

        //AddMenuItemRulesMembers(context, moduleMenu);
    }

    private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<RulesEngineResource>();

        var moduleMenu = new ApplicationMenuItem(
            RulesEngineMenus.Prefix,
            displayName: l["Menu:RulesEngine"],
            //"~/RulesEngine",
            icon: "fa fa-globe");

        //Add main menu items.
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