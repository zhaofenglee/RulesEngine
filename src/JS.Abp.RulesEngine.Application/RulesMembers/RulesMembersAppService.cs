using JS.Abp.RulesEngine.Shared;
using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using JS.Abp.RulesEngine.Permissions;
using JS.Abp.RulesEngine.RulesMembers;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.RulesMembers
{

    [Authorize(RulesEnginePermissions.RulesMembers.Default)]
    public abstract class RulesMembersAppServiceBase : ApplicationService
    {
        protected IDistributedCache<RulesMemberExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IRulesMemberRepository _rulesMemberRepository;
        protected RulesMemberManager _rulesMemberManager;
        protected IRepository<RulesGroup, Guid> _rulesGroupRepository;
        protected IRepository<Rule, Guid> _ruleRepository;

        public RulesMembersAppServiceBase(IRulesMemberRepository rulesMemberRepository, RulesMemberManager rulesMemberManager, IDistributedCache<RulesMemberExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, IRepository<RulesGroup, Guid> rulesGroupRepository, IRepository<Rule, Guid> ruleRepository)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _rulesMemberRepository = rulesMemberRepository;
            _rulesMemberManager = rulesMemberManager; _rulesGroupRepository = rulesGroupRepository;
            _ruleRepository = ruleRepository;
        }

        public virtual async Task<PagedResultDto<RulesMemberWithNavigationPropertiesDto>> GetListAsync(GetRulesMembersInput input)
        {
            var totalCount = await _rulesMemberRepository.GetCountAsync(input.FilterText, input.SequenceMin, input.SequenceMax, input.Description, input.RulesGroupId, input.RuleId);
            var items = await _rulesMemberRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.SequenceMin, input.SequenceMax, input.Description, input.RulesGroupId, input.RuleId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<RulesMemberWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<RulesMemberWithNavigationProperties>, List<RulesMemberWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<RulesMemberWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<RulesMemberWithNavigationProperties, RulesMemberWithNavigationPropertiesDto>
                (await _rulesMemberRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<RulesMemberDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<RulesMember, RulesMemberDto>(await _rulesMemberRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetRulesGroupLookupAsync(LookupRequestDto input)
        {
            var query = (await _rulesGroupRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.GroupName != null &&
                         x.GroupName.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<RulesGroup>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<RulesGroup>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetRuleLookupAsync(LookupRequestDto input)
        {
            var query = (await _ruleRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.RuleCode != null &&
                         x.RuleCode.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Rule>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Rule>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(RulesEnginePermissions.RulesMembers.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _rulesMemberRepository.DeleteAsync(id);
        }

        [Authorize(RulesEnginePermissions.RulesMembers.Create)]
        public virtual async Task<RulesMemberDto> CreateAsync(RulesMemberCreateDto input)
        {

            var rulesMember = await _rulesMemberManager.CreateAsync(
            input.RulesGroupId, input.RuleId, input.Sequence, input.Description
            );

            return ObjectMapper.Map<RulesMember, RulesMemberDto>(rulesMember);
        }

        [Authorize(RulesEnginePermissions.RulesMembers.Edit)]
        public virtual async Task<RulesMemberDto> UpdateAsync(Guid id, RulesMemberUpdateDto input)
        {

            var rulesMember = await _rulesMemberManager.UpdateAsync(
            id,
            input.RulesGroupId, input.RuleId, input.Sequence, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<RulesMember, RulesMemberDto>(rulesMember);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RulesMemberExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var rulesMembers = await _rulesMemberRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.SequenceMin, input.SequenceMax, input.Description);
            var items = rulesMembers.Select(item => new
            {
                Sequence = item.RulesMember.Sequence,
                Description = item.RulesMember.Description,

                RulesGroup = item.RulesGroup?.GroupName,
                Rule = item.Rule?.RuleCode,

            });

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(items);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "RulesMembers.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new RulesMemberExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}