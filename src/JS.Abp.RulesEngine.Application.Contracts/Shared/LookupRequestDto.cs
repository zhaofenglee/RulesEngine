using Volo.Abp.Application.Dtos;

namespace JS.Abp.RulesEngine.Shared
{
#pragma warning disable CS8618
    public abstract class LookupRequestDtoBase : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public LookupRequestDtoBase()
        {
            MaxResultCount = MaxMaxResultCount;
        }
    }
#pragma warning restore CS8618
}