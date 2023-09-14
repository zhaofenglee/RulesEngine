namespace JS.Abp.RulesEngine.RulesGroups
{
    public static class RulesGroupConsts
    {
        private const string DefaultSorting = "{0}GroupName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "RulesGroup." : string.Empty);
        }

        public const int GroupNameMaxLength = 128;
    }
}