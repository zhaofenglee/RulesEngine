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
    public class RulesAppService : RulesAppServiceBase, IRulesAppService
    {
        protected IRulesEngineStore rulesEngine => LazyServiceProvider.LazyGetRequiredService<IRulesEngineStore>();
        public RulesAppService(IRuleRepository ruleRepository, RuleManager ruleManager, IDistributedCache<RuleExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
            : base(ruleRepository, ruleManager, excelDownloadTokenCache)
        {
        }

        public virtual async Task<RuleResult> VerifyRuleAsync(VerifyRuleDto input)
        {
            
            return await rulesEngine.ExecuteRulesAsync(input.RuleCode, input.ExtraProperties);
        }
    }
}