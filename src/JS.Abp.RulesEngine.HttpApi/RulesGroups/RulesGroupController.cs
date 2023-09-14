using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using JS.Abp.RulesEngine.RulesGroups;
using Volo.Abp.Content;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.RulesGroups
{
    [RemoteService(Name = "RulesEngine")]
    [Area("rulesEngine")]
    [ControllerName("RulesGroup")]
    [Route("api/rules-engine/rules-groups")]
    public class RulesGroupController : AbpController, IRulesGroupsAppService
    {
        private readonly IRulesGroupsAppService _rulesGroupsAppService;

        public RulesGroupController(IRulesGroupsAppService rulesGroupsAppService)
        {
            _rulesGroupsAppService = rulesGroupsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<RulesGroupDto>> GetListAsync(GetRulesGroupsInput input)
        {
            return _rulesGroupsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<RulesGroupDto> GetAsync(Guid id)
        {
            return _rulesGroupsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<RulesGroupDto> CreateAsync(RulesGroupCreateDto input)
        {
            return _rulesGroupsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<RulesGroupDto> UpdateAsync(Guid id, RulesGroupUpdateDto input)
        {
            return _rulesGroupsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _rulesGroupsAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(RulesGroupExcelDownloadDto input)
        {
            return _rulesGroupsAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _rulesGroupsAppService.GetDownloadTokenAsync();
        }
    }
}