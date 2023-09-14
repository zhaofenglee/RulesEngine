using JS.Abp.RulesEngine.RuleExpressionTypes;
using JS.Abp.RulesEngine.ErrorTypes;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace JS.Abp.RulesEngine.Rules
{
    public partial interface IRuleRepository : IRepository<Rule, Guid>
    {
        Task<List<Rule>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string? filterText = null,
            string? ruleCode = null,
            string? successEvent = null,
            string? errorMessage = null,
            ErrorType? errorType = null,
            RuleExpressionType? ruleExpressionType = null,
            string? expression = null,
            string? description = null,
            CancellationToken cancellationToken = default);
    }
}