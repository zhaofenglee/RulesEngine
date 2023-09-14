using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace JS.Abp.RulesEngine.Rules;

public class RuleHandler: ILocalEventHandler<EntityChangedEventData<Rule>>,ITransientDependency
{
    private readonly IDistributedCache<RuleCacheItem> _cache;
    
    public RuleHandler(IDistributedCache<RuleCacheItem> cache)
    {
        _cache = cache;
    }
    
    public async Task HandleEventAsync(EntityChangedEventData<Rule> eventData)
    {
        await _cache.RemoveAsync(
            eventData.Entity.RuleCode
        );
    }
}