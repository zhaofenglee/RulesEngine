using Volo.Abp.Reflection;

namespace JS.Abp.RulesEngine.Permissions;

public class RulesEnginePermissions
{
    public const string GroupName = "RulesEngine";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(RulesEnginePermissions));
    }

    public static class Rules
    {
        public const string Default = GroupName + ".Rules";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RulesGroups
    {
        public const string Default = GroupName + ".RulesGroups";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class RulesMembers
    {
        public const string Default = GroupName + ".RulesMembers";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}