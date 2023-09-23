using System.Threading.Tasks;

namespace JS.Abp.RulesEngine.Rules
{
    public partial interface IRulesAppService
    {
        Task<RuleResult> VerifyRuleAsync(VerifyRuleDto input);
        //Write your custom code here...
    }
}