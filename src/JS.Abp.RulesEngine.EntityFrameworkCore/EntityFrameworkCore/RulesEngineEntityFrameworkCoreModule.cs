using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine.EntityFrameworkCore;

[DependsOn(
    typeof(RulesEngineDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class RulesEngineEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<RulesEngineDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             */
            options.AddRepository<Rule, Rules.EfCoreRuleRepository>();

            options.AddRepository<RulesGroup, RulesGroups.EfCoreRulesGroupRepository>();

            options.AddRepository<RulesMember, RulesMembers.EfCoreRulesMemberRepository>();

        });
    }
}