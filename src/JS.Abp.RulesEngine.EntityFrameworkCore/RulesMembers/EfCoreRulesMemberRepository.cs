using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using JS.Abp.RulesEngine.EntityFrameworkCore;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public abstract class EfCoreRulesMemberRepositoryBase : EfCoreRepository<RulesEngineDbContext, RulesMember, Guid>, IRulesMemberRepository
    {
        public EfCoreRulesMemberRepositoryBase(IDbContextProvider<RulesEngineDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<RulesMemberWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(rulesMember => new RulesMemberWithNavigationProperties
                {
                    RulesMember = rulesMember,
                    RulesGroup = dbContext.Set<RulesGroup>().FirstOrDefault(c => c.Id == rulesMember.RulesGroupId),
                    Rule = dbContext.Set<Rule>().FirstOrDefault(c => c.Id == rulesMember.RuleId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<RulesMemberWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? description = null,
            Guid? rulesGroupId = null,
            Guid? ruleId = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, sequenceMin, sequenceMax, description, rulesGroupId, ruleId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RulesMemberConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<RulesMemberWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from rulesMember in (await GetDbSetAsync())
                   join rulesGroup in (await GetDbContextAsync()).Set<RulesGroup>() on rulesMember.RulesGroupId equals rulesGroup.Id into rulesGroups
                   from rulesGroup in rulesGroups.DefaultIfEmpty()
                   join rule in (await GetDbContextAsync()).Set<Rule>() on rulesMember.RuleId equals rule.Id into rules
                   from rule in rules.DefaultIfEmpty()
                   select new RulesMemberWithNavigationProperties
                   {
                       RulesMember = rulesMember,
                       RulesGroup = rulesGroup,
                       Rule = rule
                   };
        }

        protected virtual IQueryable<RulesMemberWithNavigationProperties> ApplyFilter(
            IQueryable<RulesMemberWithNavigationProperties> query,
            string filterText,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? description = null,
            Guid? rulesGroupId = null,
            Guid? ruleId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.RulesMember.Description!.Contains(filterText!))
                    .WhereIf(sequenceMin.HasValue, e => e.RulesMember.Sequence >= sequenceMin!.Value)
                    .WhereIf(sequenceMax.HasValue, e => e.RulesMember.Sequence <= sequenceMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.RulesMember.Description.Contains(description))
                    .WhereIf(rulesGroupId != null && rulesGroupId != Guid.Empty, e => e.RulesGroup != null && e.RulesGroup.Id == rulesGroupId)
                    .WhereIf(ruleId != null && ruleId != Guid.Empty, e => e.Rule != null && e.Rule.Id == ruleId);
        }

        public virtual async Task<List<RulesMember>> GetListAsync(
            string? filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? description = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, sequenceMin, sequenceMax, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RulesMemberConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? description = null,
            Guid? rulesGroupId = null,
            Guid? ruleId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, sequenceMin, sequenceMax, description, rulesGroupId, ruleId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<RulesMember> ApplyFilter(
            IQueryable<RulesMember> query,
            string? filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? description = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description!.Contains(filterText!))
                    .WhereIf(sequenceMin.HasValue, e => e.Sequence >= sequenceMin!.Value)
                    .WhereIf(sequenceMax.HasValue, e => e.Sequence <= sequenceMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}