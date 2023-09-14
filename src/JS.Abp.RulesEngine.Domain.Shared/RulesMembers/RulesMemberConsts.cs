namespace JS.Abp.RulesEngine.RulesMembers
{
    public static class RulesMemberConsts
    {
        private const string DefaultSorting = "{0}Sequence asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "RulesMember." : string.Empty);
        }

    }
}