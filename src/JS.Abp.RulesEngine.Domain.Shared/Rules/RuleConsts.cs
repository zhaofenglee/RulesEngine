namespace JS.Abp.RulesEngine.Rules
{
    public static class RuleConsts
    {
        private const string DefaultSorting = "{0}RuleCode asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Rule." : string.Empty);
        }

        public const int RuleCodeMaxLength = 128;
        public const int SuccessEventMaxLength = 128;
        public const int ErrorMessageMaxLength = 512;
    }
}