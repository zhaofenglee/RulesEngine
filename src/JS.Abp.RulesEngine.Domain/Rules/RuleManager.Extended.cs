using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using Volo.Abp.Domain.Services;

namespace JS.Abp.RulesEngine.Rules
{
    public class RuleManager : RuleManagerBase
    {
        public RuleManager(IRuleRepository ruleRepository)
            : base(ruleRepository)
        {
        }
    }
}