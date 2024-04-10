using JS.Abp.RulesEngine.Shared;
using System;
using System.Threading.Tasks;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using JS.Abp.RulesEngine.RulesMembers;
using Volo.Abp.Content;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.RulesMembers
{
    [RemoteService(Name = "RulesEngine")]
    [Area("rulesEngine")]
    [ControllerName("RulesMember")]
    [Route("api/rules-engine/rules-members")]
    public class RulesMemberController : AbpController, IRulesMembersAppService
    {
        private readonly IRulesMembersAppService _rulesMembersAppService;

        public RulesMemberController(IRulesMembersAppService rulesMembersAppService)
        {
            _rulesMembersAppService = rulesMembersAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<RulesMemberWithNavigationPropertiesDto>> GetListAsync(GetRulesMembersInput input)
        {
            return _rulesMembersAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<RulesMemberWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _rulesMembersAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<RulesMemberDto> GetAsync(Guid id)
        {
            return _rulesMembersAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("rules-group-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetRulesGroupLookupAsync(LookupRequestDto input)
        {
            return _rulesMembersAppService.GetRulesGroupLookupAsync(input);
        }

        [HttpGet]
        [Route("rule-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetRuleLookupAsync(LookupRequestDto input)
        {
            return _rulesMembersAppService.GetRuleLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<RulesMemberDto> CreateAsync(RulesMemberCreateDto input)
        {
            return _rulesMembersAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<RulesMemberDto> UpdateAsync(Guid id, RulesMemberUpdateDto input)
        {
            return _rulesMembersAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _rulesMembersAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("as-excel-file")]
        public virtual Task<IRemoteStreamContent> GetListAsExcelFileAsync(RulesMemberExcelDownloadDto input)
        {
            return _rulesMembersAppService.GetListAsExcelFileAsync(input);
        }

        [HttpGet]
        [Route("download-token")]
        public virtual Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            return _rulesMembersAppService.GetDownloadTokenAsync();
        }
    }
}