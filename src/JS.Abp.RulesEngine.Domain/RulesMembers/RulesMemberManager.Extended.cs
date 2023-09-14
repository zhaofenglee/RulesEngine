using Volo.Abp.Domain.Services;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public class RulesMemberManager : RulesMemberManagerBase
    {
        public RulesMemberManager(IRulesMemberRepository rulesMemberRepository)
            : base(rulesMemberRepository)
        {
        }
    }
}