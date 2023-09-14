using JS.Abp.RulesEngine.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace JS.Abp.RulesEngine.Permissions;

public class RulesEnginePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(RulesEnginePermissions.GroupName, L("Permission:RulesEngine"));

        var rulePermission = myGroup.AddPermission(RulesEnginePermissions.Rules.Default, L("Permission:Rules"));
        rulePermission.AddChild(RulesEnginePermissions.Rules.Create, L("Permission:Create"));
        rulePermission.AddChild(RulesEnginePermissions.Rules.Edit, L("Permission:Edit"));
        rulePermission.AddChild(RulesEnginePermissions.Rules.Delete, L("Permission:Delete"));

        var rulesGroupPermission = myGroup.AddPermission(RulesEnginePermissions.RulesGroups.Default, L("Permission:RulesGroups"));
        rulesGroupPermission.AddChild(RulesEnginePermissions.RulesGroups.Create, L("Permission:Create"));
        rulesGroupPermission.AddChild(RulesEnginePermissions.RulesGroups.Edit, L("Permission:Edit"));
        rulesGroupPermission.AddChild(RulesEnginePermissions.RulesGroups.Delete, L("Permission:Delete"));

        var rulesMemberPermission = myGroup.AddPermission(RulesEnginePermissions.RulesMembers.Default, L("Permission:RulesMembers"));
        rulesMemberPermission.AddChild(RulesEnginePermissions.RulesMembers.Create, L("Permission:Create"));
        rulesMemberPermission.AddChild(RulesEnginePermissions.RulesMembers.Edit, L("Permission:Edit"));
        rulesMemberPermission.AddChild(RulesEnginePermissions.RulesMembers.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<RulesEngineResource>(name);
    }
}