using JS.Abp.RulesEngine.Localization;
using Volo.Abp.Application.Services;

namespace JS.Abp.RulesEngine;

public abstract class RulesEngineAppService : ApplicationService
{
    protected RulesEngineAppService()
    {
        LocalizationResource = typeof(RulesEngineResource);
        ObjectMapperContext = typeof(RulesEngineApplicationModule);
    }
}
