using Volo.Abp.Bundling;

namespace JS.Abp.RulesEngine.Blazor.Host.Client;

public class RulesEngineBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
