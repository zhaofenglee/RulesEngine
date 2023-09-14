using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace JS.Abp.RulesEngine.MongoDB;

[DependsOn(
    typeof(RulesEngineDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class RulesEngineMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<RulesEngineMongoDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, MongoQuestionRepository>();
             */
            options.AddRepository<Rule, Rules.MongoRuleRepository>();

            options.AddRepository<RulesGroup, RulesGroups.MongoRulesGroupRepository>();

            options.AddRepository<RulesMember, RulesMembers.MongoRulesMemberRepository>();

        });
    }
}