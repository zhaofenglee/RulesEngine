using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using JS.Abp.RulesEngine.Rules;
using Volo.Abp.Content;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.Rules
{
    [RemoteService(Name = "RulesEngine")]
    [Area("rulesEngine")]
    [ControllerName("Rule")]
    [Route("api/rules-engine/rules")]
    public class RuleController : AbpController, IRulesAppService
    {
        private readonly IRulesAppService _rulesAppService;

        public RuleController(IRulesAppService rulesAppService)
        {
            _rulesAppService = rulesAppService;
        }
        [HttpGet]
        [Route("verify-rule")]
        public virtual Task<RuleResult> VerifyRuleAsync(VerifyRuleDto input)
        {
            return _rulesAppService.VerifyRuleAsync(input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<RuleDto>> GetListAsync(GetRulesInput input)
        {
            return _rulesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<RuleDto> GetAsync(Guid id)
        {
            return _rulesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<RuleDto> CreateAsync(RuleCreateDto input)
        {
            return _rulesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<RuleDto> UpdateAsync(Guid id, RuleUpdateDto input)
        {
            return _rulesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _rulesAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(RuleExcelDownloadDto input)
        {
            return _rulesAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _rulesAppService.GetDownloadTokenAsync();
        }
    }
}