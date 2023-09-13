using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace JS.Abp.RulesEngine.Blazor.Menus;

public class RulesEngineMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(RulesEngineMenus.Prefix, displayName: "RulesEngine", "/RulesEngine", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
