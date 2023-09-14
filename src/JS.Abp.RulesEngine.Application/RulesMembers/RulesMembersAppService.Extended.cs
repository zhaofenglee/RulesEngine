using JS.Abp.RulesEngine.Shared;
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
using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesMembers;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public class RulesMembersAppService : RulesMembersAppServiceBase, IRulesMembersAppService
    {
        public RulesMembersAppService(IRulesMemberRepository rulesMemberRepository,
            RulesMemberManager rulesMemberManager, 
            IDistributedCache<RulesMemberExcelDownloadTokenCacheItem, string> excelDownloadTokenCache, 
            IRepository<RulesGroup, Guid> rulesGroupRepository,
            IRepository<Rule, Guid> ruleRepository)
            : base(rulesMemberRepository, rulesMemberManager, excelDownloadTokenCache, rulesGroupRepository,ruleRepository)
        {
        }
    }
}