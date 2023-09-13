using JS.Abp.RulesEngine.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace JS.Abp.RulesEngine;

public abstract class RulesEngineController : AbpControllerBase
{
    protected RulesEngineController()
    {
        LocalizationResource = typeof(RulesEngineResource);
    }
}
