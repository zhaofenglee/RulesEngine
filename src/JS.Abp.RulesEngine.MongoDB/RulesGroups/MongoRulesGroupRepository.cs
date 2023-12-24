using JS.Abp.RulesEngine.OperatorTypes;
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

namespace JS.Abp.RulesEngine.RulesGroups
{
    public class MongoRulesGroupRepository : MongoDbRepository<RulesEngineMongoDbContext, RulesGroup, Guid>, IRulesGroupRepository
    {
        public MongoRulesGroupRepository(IMongoDbContextProvider<RulesEngineMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<List<RulesGroup>> GetListAsync(
            string? filterText = null,
            string? groupName = null,
            OperatorType? operatorType = null,
            string? description = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, groupName, operatorType, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RulesGroupConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<RulesGroup>>()
                .PageBy<RulesGroup, IMongoQueryable<RulesGroup>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? groupName = null,
            OperatorType? operatorType = null,
            string? description = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, groupName, operatorType, description);
            return await query.As<IMongoQueryable<RulesGroup>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<RulesGroup> ApplyFilter(
            IQueryable<RulesGroup> query,
            string? filterText = null,
            string? groupName = null,
            OperatorType? operatorType = null,
            string? description = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.GroupName!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(groupName), e => e.GroupName.Contains(groupName))
                    .WhereIf(operatorType.HasValue, e => e.OperatorType == operatorType)
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}