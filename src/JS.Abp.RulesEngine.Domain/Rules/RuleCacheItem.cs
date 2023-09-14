using JetBrains.Annotations;
using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using Volo.Abp.Caching;

namespace JS.Abp.RulesEngine.Rules;

[CacheName("Rule")]
public class RuleCacheItem
{
    [NotNull]
    public virtual string RuleName { get; set; }

    [NotNull]
    public virtual string SuccessEvent { get; set; }

    [NotNull]
    public virtual string ErrorMessage { get; set; }

    public virtual ErrorType ErrorType { get; set; }

    public virtual RuleExpressionType RuleExpressionType { get; set; }

    [CanBeNull]
    public virtual string? Expression { get; set; }
    
    public RuleCacheItem(string ruleName, string successEvent, string errorMessage, ErrorType errorType, RuleExpressionType ruleExpressionType, string expression)
    {
        RuleName = ruleName;
        SuccessEvent = successEvent;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
        RuleExpressionType = ruleExpressionType;
        Expression = expression;
    }
}