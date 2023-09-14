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
using JS.Abp.RulesEngine.Rules;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.Rules
{

    [Authorize(RulesEnginePermissions.Rules.Default)]
    public abstract class RulesAppServiceBase : ApplicationService
    {
        protected IDistributedCache<RuleExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IRuleRepository _ruleRepository;
        protected RuleManager _ruleManager;

        public RulesAppServiceBase(IRuleRepository ruleRepository, RuleManager ruleManager, IDistributedCache<RuleExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _ruleRepository = ruleRepository;
            _ruleManager = ruleManager;
        }

        public virtual async Task<PagedResultDto<RuleDto>> GetListAsync(GetRulesInput input)
        {
            var totalCount = await _ruleRepository.GetCountAsync(input.FilterText, input.RuleCode, input.SuccessEvent, input.ErrorMessage, input.ErrorType, input.RuleExpressionType, input.Expression, input.Description);
            var items = await _ruleRepository.GetListAsync(input.FilterText, input.RuleCode, input.SuccessEvent, input.ErrorMessage, input.ErrorType, input.RuleExpressionType, input.Expression, input.Description, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<RuleDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Rule>, List<RuleDto>>(items)
            };
        }

        public virtual async Task<RuleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Rule, RuleDto>(await _ruleRepository.GetAsync(id));
        }

        [Authorize(RulesEnginePermissions.Rules.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _ruleRepository.DeleteAsync(id);
        }

        [Authorize(RulesEnginePermissions.Rules.Create)]
        public virtual async Task<RuleDto> CreateAsync(RuleCreateDto input)
        {

            var rule = await _ruleManager.CreateAsync(
            input.RuleCode, input.SuccessEvent, input.ErrorMessage, input.ErrorType, input.RuleExpressionType, input.Expression, input.Description
            );

            return ObjectMapper.Map<Rule, RuleDto>(rule);
        }

        [Authorize(RulesEnginePermissions.Rules.Edit)]
        public virtual async Task<RuleDto> UpdateAsync(Guid id, RuleUpdateDto input)
        {

            var rule = await _ruleManager.UpdateAsync(
            id,
            input.RuleCode, input.SuccessEvent, input.ErrorMessage, input.ErrorType, input.RuleExpressionType, input.Expression, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Rule, RuleDto>(rule);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RuleExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _ruleRepository.GetListAsync(input.FilterText, input.RuleCode, input.SuccessEvent, input.ErrorMessage, input.ErrorType, input.RuleExpressionType, input.Expression, input.Description);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<Rule>, List<RuleExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "Rules.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new RuleExcelDownloadTokenCacheItem { Token = token },
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