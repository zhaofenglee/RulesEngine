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
        });
    }
}
