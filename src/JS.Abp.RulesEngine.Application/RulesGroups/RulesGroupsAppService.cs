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
using JS.Abp.RulesEngine.RulesGroups;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.RulesGroups
{

    [Authorize(RulesEnginePermissions.RulesGroups.Default)]
    public abstract class RulesGroupsAppServiceBase : ApplicationService
    {
        protected IDistributedCache<RulesGroupExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        protected IRulesGroupRepository _rulesGroupRepository;
        protected RulesGroupManager _rulesGroupManager;

        public RulesGroupsAppServiceBase(IRulesGroupRepository rulesGroupRepository, RulesGroupManager rulesGroupManager, IDistributedCache<RulesGroupExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _rulesGroupRepository = rulesGroupRepository;
            _rulesGroupManager = rulesGroupManager;
        }

        public virtual async Task<PagedResultDto<RulesGroupDto>> GetListAsync(GetRulesGroupsInput input)
        {
            var totalCount = await _rulesGroupRepository.GetCountAsync(input.FilterText, input.GroupName, input.OperatorType, input.Description);
            var items = await _rulesGroupRepository.GetListAsync(input.FilterText, input.GroupName, input.OperatorType, input.Description, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<RulesGroupDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<RulesGroup>, List<RulesGroupDto>>(items)
            };
        }

        public virtual async Task<RulesGroupDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<RulesGroup, RulesGroupDto>(await _rulesGroupRepository.GetAsync(id));
        }

        [Authorize(RulesEnginePermissions.RulesGroups.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _rulesGroupRepository.DeleteAsync(id);
        }

        [Authorize(RulesEnginePermissions.RulesGroups.Create)]
        public virtual async Task<RulesGroupDto> CreateAsync(RulesGroupCreateDto input)
        {

            var rulesGroup = await _rulesGroupManager.CreateAsync(
            input.GroupName, input.OperatorType, input.Description
            );

            return ObjectMapper.Map<RulesGroup, RulesGroupDto>(rulesGroup);
        }

        [Authorize(RulesEnginePermissions.RulesGroups.Edit)]
        public virtual async Task<RulesGroupDto> UpdateAsync(Guid id, RulesGroupUpdateDto input)
        {

            var rulesGroup = await _rulesGroupManager.UpdateAsync(
            id,
            input.GroupName, input.OperatorType, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<RulesGroup, RulesGroupDto>(rulesGroup);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(RulesGroupExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _rulesGroupRepository.GetListAsync(input.FilterText, input.GroupName, input.OperatorType, input.Description);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<RulesGroup>, List<RulesGroupExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "RulesGroups.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public virtual async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new RulesGroupExcelDownloadTokenCacheItem { Token = token },
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