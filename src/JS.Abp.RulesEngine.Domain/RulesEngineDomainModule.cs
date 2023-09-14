using System;
using JS.Abp.RulesEngine.Rules;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Caching;
using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpCachingModule),
    typeof(RulesEngineDomainSharedModule)
)]
public class RulesEngineDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var options = context.Services.ExecutePreConfiguredActions<RulesEngineOptions>();
        context.Services.AddEntityCache<Rule, Guid>( new DistributedCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromMinutes(options.CacheExpirationTime)
        });

    }
}
