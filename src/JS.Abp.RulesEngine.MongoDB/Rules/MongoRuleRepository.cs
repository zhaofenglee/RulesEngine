using JS.Abp.RulesEngine.RuleExpressionTypes;
using JS.Abp.RulesEngine.ErrorTypes;
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

namespace JS.Abp.RulesEngine.Rules
{
    public abstract class MongoRuleRepository : MongoDbRepository<RulesEngineMongoDbContext, Rule, Guid>, IRuleRepository
    {
        public MongoRuleRepository(IMongoDbContextProvider<RulesEngineMongoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public virtual async Task<List<Rule>> GetListAsync(
            string? filterText = null,
            string? ruleCode = null,
            string? successEvent = null,
            string? errorMessage = null,
            ErrorType? errorType = null,
            RuleExpressionType? ruleExpressionType = null,
            string? expression = null,
            string? description = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, ruleCode, successEvent, errorMessage, errorType, ruleExpressionType, expression, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RuleConsts.GetDefaultSorting(false) : sorting);
            return await query.As<IMongoQueryable<Rule>>()
                .PageBy<Rule, IMongoQueryable<Rule>>(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? ruleCode = null,
            string? successEvent = null,
            string? errorMessage = null,
            ErrorType? errorType = null,
            RuleExpressionType? ruleExpressionType = null,
            string? expression = null,
            string? description = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetMongoQueryableAsync(cancellationToken)), filterText, ruleCode, successEvent, errorMessage, errorType, ruleExpressionType, expression, description);
            return await query.As<IMongoQueryable<Rule>>().LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Rule> ApplyFilter(
            IQueryable<Rule> query,
            string? filterText = null,
            string? ruleCode = null,
            string? successEvent = null,
            string? errorMessage = null,
            ErrorType? errorType = null,
            RuleExpressionType? ruleExpressionType = null,
            string? expression = null,
            string? description = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.RuleCode!.Contains(filterText!) || e.SuccessEvent!.Contains(filterText!) || e.ErrorMessage!.Contains(filterText!) || e.Expression!.Contains(filterText!) || e.Description!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(ruleCode), e => e.RuleCode.Contains(ruleCode))
                    .WhereIf(!string.IsNullOrWhiteSpace(successEvent), e => e.SuccessEvent.Contains(successEvent))
                    .WhereIf(!string.IsNullOrWhiteSpace(errorMessage), e => e.ErrorMessage.Contains(errorMessage))
                    .WhereIf(errorType.HasValue, e => e.ErrorType == errorType)
                    .WhereIf(ruleExpressionType.HasValue, e => e.RuleExpressionType == ruleExpressionType)
                    .WhereIf(!string.IsNullOrWhiteSpace(expression), e => e.Expression.Contains(expression))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description));
        }
    }
}