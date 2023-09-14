using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.RulesGroups
{
    public partial interface IRulesGroupsAppService : IApplicationService
    {
        Task<PagedResultDto<RulesGroupDto>> GetListAsync(GetRulesGroupsInput input);

        Task<RulesGroupDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<RulesGroupDto> CreateAsync(RulesGroupCreateDto input);

        Task<RulesGroupDto> UpdateAsync(Guid id, RulesGroupUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(RulesGroupExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}