namespace JS.Abp.RulesEngine.Shared
{
#pragma warning disable CS8618
    public abstract class LookupDtoBase<TKey>
    {
        public TKey Id { get; set; }

        public string DisplayName { get; set; }
    }
#pragma warning restore CS8618
}