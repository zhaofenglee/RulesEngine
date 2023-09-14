using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JS.Abp.RulesEngine.OperatorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.RulesMembers;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Caching;
using Volo.Abp.Domain.Repositories;

namespace JS.Abp.RulesEngine;

public class RulesEngineStore:IRulesEngineStore,ITransientDependency
{
    protected IRuleRepository _ruleRepository;
    protected IRulesGroupRepository _rulesGroupRepository;
    protected IRulesMemberRepository _rulesMemberRepository;
    protected RulesEngineOptions Options { get; }
    private readonly IDistributedCache<RuleCacheItem> _cache;
    private readonly IEntityCache<Rule, Guid> _ruleCache;

    public RulesEngineStore(IRuleRepository ruleRepository,
        IRulesGroupRepository rulesGroupRepository,
        IRulesMemberRepository rulesMemberRepository,
        IOptions<RulesEngineOptions> options,
        IDistributedCache<RuleCacheItem> cache,
        IEntityCache<Rule, Guid> ruleCache)
    {
        _ruleRepository = ruleRepository;
        _rulesGroupRepository = rulesGroupRepository;
        _rulesMemberRepository = rulesMemberRepository;
        Options = options.Value;
        _cache = cache;
        _ruleCache = ruleCache;
    }
    public virtual async Task<RuleResult> ExecuteRulesAsync<TDto>(string ruleCode, TDto input)
    {
        var  rule =  await _cache.GetOrAddAsync(
            ruleCode, 
            async () => await GetRuleFromDatabaseAsync(ruleCode),
            () => new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Options.CacheExpirationTime)
            }
        );
        if (rule==null)
        {
            return new RuleResult(true, null, "Rule not found", ErrorTypes.ErrorType.None);
        }
        else
        {
            var result = await ContainsRuleAsync(rule,input);
            if (result)
            {
                return new RuleResult(true, rule.SuccessEvent, null, ErrorTypes.ErrorType.None);
            }
            else
            {
                return new RuleResult(false, null, rule.ErrorMessage, rule.ErrorType);
            }
        }
        
    }

    public  virtual async Task<RuleResult> ExecuteRulesAsync<TDto>(Guid id, TDto input)
    {
        var rule = await _ruleCache.FindAsync(id);
        if (rule == null)
        {
            return new RuleResult(true, null, "Rule not found", ErrorTypes.ErrorType.None);
        }
        else
        {
            return await ExecuteRulesAsync(rule.RuleCode, input);
        }
    }

    public virtual async Task<List<RuleResult>> ExecuteAllRulesAsync<TDto>(string ruleCode, IEnumerable<TDto> input)
    {
        var result = new List<RuleResult>();
        foreach (var item in input)
        {
            var ruleResult = await ExecuteRulesAsync(ruleCode, item);
            result.Add(ruleResult);
        }
        return result;

    }

    public virtual async Task<List<RuleResult>> ExecuteAllRulesAsync<TDto>(Guid id, IEnumerable<TDto> input)
    {
        var result = new List<RuleResult>();
        foreach (var item in input)
        {
            var ruleResult = await ExecuteRulesAsync(id, item);
            result.Add(ruleResult);
        }
        return result;
    }

    public virtual async Task<RuleResult> ExecuteRulesGroupAsync<TDto>(string ruleGroupName, TDto input)
    {
        var rulesGroups = await _rulesGroupRepository.GetListAsync(ruleGroupName);
        if (rulesGroups.Any())
        {
            var rulesGroup = rulesGroups.First();
            var rulesMembers = await _rulesMemberRepository.GetListAsync(c => c.RulesGroupId == rulesGroup.Id);
            if (rulesMembers.Any())
            {
                var result = new RuleResult();
                foreach (var rulesMember in rulesMembers)
                {
                    var ruleResult = await ExecuteRulesAsync(rulesMember.RuleId!.Value, input);
                    //如果是And运算符，需要全部满足，OR运算符只要有一个满足则返回
                    if (ruleResult.IsSuccess && rulesGroup.OperatorType == OperatorType.Or)
                    {
                        return ruleResult;
                    }
                    else if (!ruleResult.IsSuccess && rulesGroup.OperatorType == OperatorType.And)
                    {
                        return ruleResult;
                    }
                    else
                    {
                        result = ruleResult;
                    }
                    
                } 
                return result;
            }
            else
            {
                return new RuleResult(true, null, "RulesMember not found", ErrorTypes.ErrorType.None);
            }
        }
        else
        {
            return new RuleResult(true, null, "RulesGroup not found", ErrorTypes.ErrorType.None);
        }
    }

    public virtual async Task<List<RuleResult>> ExecuteAllRulesGroupAsync<TDto>(string ruleGroupName, IEnumerable<TDto> input)
    {
        var result = new List<RuleResult>();
        foreach (var item in input)
        {
            var ruleResult = await ExecuteRulesGroupAsync(ruleGroupName, item);
            result.Add(ruleResult);
        }
        return result;
    }

    private async Task<RuleCacheItem?> GetRuleFromDatabaseAsync(string ruleCode)
    {
        var rules = await _ruleRepository.GetListAsync(c=>c.RuleCode==ruleCode);
        if (!rules.Any())
        {
            return null;
        }
        else
        {
            var rule = rules.First();
            return new RuleCacheItem(rule.RuleCode, rule.SuccessEvent, rule.ErrorMessage, rule.ErrorType, rule.RuleExpressionType, rule.Expression);
        }
    }
    
    private async Task<bool> ContainsRuleAsync<TDto>(RuleCacheItem rule,TDto input)
    {
        if (rule.RuleExpressionType == RuleExpressionType.TextExpression)
        {
            return rule.Expression == input!.ToString();
        }
        else
        {
            var func = (Func<TDto, bool>)DynamicExpressionParser.ParseLambda(
                typeof(Func<TDto, bool>),
                new[]
                {
                    Expression.Parameter(typeof(TDto), "x")
                },
                null,
                rule.Expression!
            ).Compile();
            var result =  func(input);
        
            return result;
            
        }
     
    }
}