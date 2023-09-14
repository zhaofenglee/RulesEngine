using JS.Abp.RulesEngine.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public partial interface IRulesMembersAppService : IApplicationService
    {
        Task<PagedResultDto<RulesMemberWithNavigationPropertiesDto>> GetListAsync(GetRulesMembersInput input);

        Task<RulesMemberWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<RulesMemberDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetRulesGroupLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetRuleLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<RulesMemberDto> CreateAsync(RulesMemberCreateDto input);

        Task<RulesMemberDto> UpdateAsync(Guid id, RulesMemberUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(RulesMemberExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}