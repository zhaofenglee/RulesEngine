using JS.Abp.RulesEngine.ErrorTypes;
using Volo.Abp.Domain.Services;

namespace JS.Abp.RulesEngine.RulesGroups
{
    public class RulesGroupManager : RulesGroupManagerBase
    {
        public RulesGroupManager(IRulesGroupRepository rulesGroupRepository)
            : base(rulesGroupRepository)
        {
        }
    }
}