using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using JS.Abp.RulesEngine.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public class MongoRulesMemberRepository : MongoDbRepository<RulesEngineMongoDbContext, RulesMember, Guid>, IRulesMemberRepository
    {
        public MongoRulesMemberRepository(IMongoDbContextProvider<RulesEngineMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<RulesMemberWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var rulesMember = await (await GetMongoQueryableAsync(cancellationToken))
                .FirstOrDefaultAsync(e => e.Id == id, GetCancellationToken(cancellationToken));

            var rulesGroup = await (await GetDbContextAsync(cancellationToken)).Collection<RulesGroup>().AsQueryable().FirstOrDefaultAsync(e => e.Id == rulesMember.RulesGroupId, cancellationToken: cancellationToken);
            var rule = await (await GetDbContextAsync(cancellationToken)).Collection<Rule>().AsQueryable().FirstOrDefaultAsync(e => e.Id == rulesMember.RuleId, cancellationToken: cancellationToken);

            return new RulesMemberWithNavigationProperties
            {
                RulesMember = rulesMember,
                RulesGroup = rulesGroup,
                Rule = rule,

            };
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, sequenceMin, sequenceMax, description, rulesGroupId, ruleId);
            var rulesMembers = await query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RulesMemberConsts.GetDefaultSorting(false) : sorting.Split('.').Last())
                .As<IMongoQueryable<RulesMember>>()
                .PageBy<RulesMember, IMongoQueryable<RulesMember>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));

            var dbContext = await GetDbContextAsync(cancellationToken);
            return rulesMembers.Select(s => new RulesMemberWithNavigationProperties
            {
                RulesMember = s,
                RulesGroup = dbContext.Collection<RulesGroup>().AsQueryable().FirstOrDefault(e => e.Id == s.RulesGroupId),
                Rule = dbContext.Collection<Rule>().AsQueryable().FirstOrDefault(e => e.Id == s.RuleId),

            }).ToList();
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, sequenceMin, sequenceMax, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RulesMemberConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<RulesMember>>()
                .PageBy<RulesMember, IMongoQueryable<RulesMember>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
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
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, sequenceMin, sequenceMax, description, rulesGroupId, ruleId);
            return await query.As<IMongoQueryable<RulesMember>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<RulesMember> ApplyFilter(
            IQueryable<RulesMember> query,
            string? filterText = null,
            int? sequenceMin = null,
            int? sequenceMax = null,
            string? description = null,
            Guid? rulesGroupId = null,
            Guid? ruleId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Description!.Contains(filterText!))
                    .WhereIf(sequenceMin.HasValue, e => e.Sequence >= sequenceMin!.Value)
                    .WhereIf(sequenceMax.HasValue, e => e.Sequence <= sequenceMax!.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(rulesGroupId != null && rulesGroupId != Guid.Empty, e => e.RulesGroupId == rulesGroupId)
                    .WhereIf(ruleId != null && ruleId != Guid.Empty, e => e.RuleId == ruleId);
        }
    }
}