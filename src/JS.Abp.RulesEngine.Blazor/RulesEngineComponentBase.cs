﻿using JS.Abp.RulesEngine.Localization;
using Volo.Abp.AspNetCore.Components;

namespace JS.Abp.RulesEngine.Blazor;

public abstract class RulesEngineComponentBase : AbpComponentBase
{
    protected RulesEngineComponentBase()
    {
        LocalizationResource = typeof(RulesEngineResource);
    }
}
