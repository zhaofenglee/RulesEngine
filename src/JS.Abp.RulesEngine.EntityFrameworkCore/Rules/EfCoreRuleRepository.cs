using JS.Abp.RulesEngine.RuleExpressionTypes;
using JS.Abp.RulesEngine.ErrorTypes;
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

namespace JS.Abp.RulesEngine.Rules
{
    public abstract class EfCoreRuleRepositoryBase : EfCoreRepository<RulesEngineDbContext, Rule, Guid>, IRuleRepository
    {
        public EfCoreRuleRepositoryBase(IDbContextProvider<RulesEngineDbContext> dbContextProvider)
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
            var query = ApplyFilter((await GetQueryableAsync()), filterText, ruleCode, successEvent, errorMessage, errorType, ruleExpressionType, expression, description);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? RuleConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
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
            var query = ApplyFilter((await GetDbSetAsync()), filterText, ruleCode, successEvent, errorMessage, errorType, ruleExpressionType, expression, description);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
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