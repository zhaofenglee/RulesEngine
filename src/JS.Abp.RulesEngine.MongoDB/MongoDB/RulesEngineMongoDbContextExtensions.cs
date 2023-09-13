using Volo.Abp;
using Volo.Abp.MongoDB;

namespace JS.Abp.RulesEngine.MongoDB;

public static class RulesEngineMongoDbContextExtensions
{
    public static void ConfigureRulesEngine(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
