using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class RulesEngineApplicationTestBase<TStartupModule> : RulesEngineTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
