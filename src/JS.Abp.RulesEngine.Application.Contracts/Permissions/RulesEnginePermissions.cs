using Volo.Abp.Reflection;

namespace JS.Abp.RulesEngine.Permissions;

public class RulesEnginePermissions
{
    public const string GroupName = "RulesEngine";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(RulesEnginePermissions));
    }
}
