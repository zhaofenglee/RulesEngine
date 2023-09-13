using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace JS.Abp.RulesEngine;

[Dependency(ReplaceServices = true)]
public class RulesEngineBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "RulesEngine";
}
