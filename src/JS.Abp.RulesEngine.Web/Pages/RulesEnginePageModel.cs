using JS.Abp.RulesEngine.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace JS.Abp.RulesEngine.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class RulesEnginePageModel : AbpPageModel
{
    protected RulesEnginePageModel()
    {
        LocalizationResourceType = typeof(RulesEngineResource);
        ObjectMapperContext = typeof(RulesEngineWebModule);
    }
}
