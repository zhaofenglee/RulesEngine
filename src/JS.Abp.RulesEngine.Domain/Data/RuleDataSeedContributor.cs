using System.Linq;
using System.Threading.Tasks;
using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.RulesMembers;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace JS.Abp.RulesEngine.Data;

public class RuleDataSeedContributor: IDataSeedContributor, ITransientDependency
{
    private readonly IRuleRepository _ruleRepository;
    private readonly RuleManager _ruleManager;
    private readonly RulesGroupManager _ruleGroupManager;
    private readonly RulesMemberManager _ruleMemberManager;
    private readonly ICurrentTenant _currentTenant;
    public RuleDataSeedContributor(
        IRuleRepository ruleRepository,
        RuleManager ruleManager,
        RulesGroupManager ruleGroupManager,
        RulesMemberManager ruleMemberManager,
        ICurrentTenant currentTenant)
    {
        _ruleRepository = ruleRepository;
        _ruleManager = ruleManager;
        _ruleGroupManager = ruleGroupManager;
        _ruleMemberManager = ruleMemberManager;
        _currentTenant = currentTenant;
    }
    
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            if (!(await _ruleRepository.GetListAsync()).Any())
            {
                //如果没有规则，则添加规则
                //建一个满1000减500的规则
                var rule1 =  await _ruleManager.CreateAsync("1000-500", "-500", "不满足", 0, 0, "Convert.ToInt32(x.Price) >=1000",
                    "满1000减500的规则");
               //建一个满500减300的规则
               var rule2 =  await _ruleManager.CreateAsync("500-300", "-300", "不满足", 0, 0, "Convert.ToInt32(x.Price) >=500",
                    "满500减300的规则");
                //建一个满300减100的规则
                var rule3 =  await _ruleManager.CreateAsync("300-100", "-100", "不满足", 0, 0, "Convert.ToInt32(x.Price) >=300",
                    "满300减100的规则");
                //建一个满200减50的规则
                var rule4 =  await _ruleManager.CreateAsync("200-50", "-50", "不满足", 0, 0, "Convert.ToInt32(x.Price) >=200",
                    "满200减50的规则");
                //建一个满100减10的规则
                var rule5 =  await _ruleManager.CreateAsync("100-10", "-10", "不满足", 0, 0, "Convert.ToInt32(x.Price) >=100",
                    "满100减10的规则");
                //建一个满50减5的规则
                var rule6 =  await _ruleManager.CreateAsync("50-5", "-5", "不满足", 0, 0, "Convert.ToInt32(x.Price) >=50",
                    "满50减5的规则");
                //建一个满30减3的规则
                var rule7 =  await _ruleManager.CreateAsync("30-3", "-3", "不满足", 0, 0, "Convert.ToInt32(x.Price) >=30",
                    "满30减3的规则");
                //建一个满20减2的规则
                var rule8 =  await _ruleManager.CreateAsync("20-2", "-2", "不满足", 0, 0, "Convert.ToInt32(x.Price) >=20",
                    "满20减2的规则");
                
                //建一个规则组
                var ruleGroup = await _ruleGroupManager.CreateAsync("TestPrice", 0, "满减规则组");
                
                //将规则添加到规则组
                
                await _ruleMemberManager.CreateAsync(ruleGroup.Id, rule1.Id, 0, "满1000减500");
                await _ruleMemberManager.CreateAsync(ruleGroup.Id, rule2.Id, 1, "满500减300");
                await _ruleMemberManager.CreateAsync(ruleGroup.Id, rule3.Id, 2, "满300减100");
                await _ruleMemberManager.CreateAsync(ruleGroup.Id, rule4.Id, 3, "满200减50");
                await _ruleMemberManager.CreateAsync(ruleGroup.Id, rule5.Id, 4, "满100减10");
                await _ruleMemberManager.CreateAsync(ruleGroup.Id, rule6.Id, 5, "满50减5");
                await _ruleMemberManager.CreateAsync(ruleGroup.Id, rule7.Id, 6, "满30减3");
                await _ruleMemberManager.CreateAsync(ruleGroup.Id, rule8.Id, 7, "满20减2");
                

            }
        }
    }
}