using System.Threading.Tasks;

namespace JS.Abp.RulesEngine.RulesGroups
{
    public partial interface IRulesGroupsAppService
    {
        Task<RuleResult> VerifyRulesGroupAsync(VerifyRuleGroupDto input);
        //Write your custom code here...
    }
}