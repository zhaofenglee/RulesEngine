using JS.Abp.RulesEngine.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JS.Abp.RulesEngine.Pages;

public abstract class RulesEnginePageModel : AbpPageModel
{
    protected RulesEnginePageModel()
    {
        LocalizationResourceType = typeof(RulesEngineResource);
    }
}
