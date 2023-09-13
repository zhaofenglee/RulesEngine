using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace JS.Abp.RulesEngine.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class RulesEngineBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "RulesEngine";
}
