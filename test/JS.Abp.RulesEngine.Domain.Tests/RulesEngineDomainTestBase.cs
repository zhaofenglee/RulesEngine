using Volo.Abp.Modularity;

namespace JS.Abp.RulesEngine;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class RulesEngineDomainTestBase<TStartupModule> : RulesEngineTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
