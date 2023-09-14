using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.Rules
{
    public partial interface IRulesAppService : IApplicationService
    {
        Task<PagedResultDto<RuleDto>> GetListAsync(GetRulesInput input);

        Task<RuleDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<RuleDto> CreateAsync(RuleCreateDto input);

        Task<RuleDto> UpdateAsync(Guid id, RuleUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(RuleExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}