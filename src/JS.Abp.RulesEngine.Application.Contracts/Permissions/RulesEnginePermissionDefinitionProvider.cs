using JS.Abp.RulesEngine.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace JS.Abp.RulesEngine.Permissions;

public class RulesEnginePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(RulesEnginePermissions.GroupName, L("Permission:RulesEngine"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RulesEngineResource>(name);
    }
}
