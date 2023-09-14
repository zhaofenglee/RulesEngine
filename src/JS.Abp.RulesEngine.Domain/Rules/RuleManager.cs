using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace JS.Abp.RulesEngine.Rules
{
    public abstract class RuleManagerBase : DomainService
    {
        protected IRuleRepository _ruleRepository;

        public RuleManagerBase(IRuleRepository ruleRepository)
        {
            _ruleRepository = ruleRepository;
        }

        public virtual async Task<Rule> CreateAsync(
        string ruleCode, string successEvent, string errorMessage, ErrorType errorType, RuleExpressionType ruleExpressionType, string expression, string description)
        {
            Check.NotNullOrWhiteSpace(ruleCode, nameof(ruleCode));
            Check.Length(ruleCode, nameof(ruleCode), RuleConsts.RuleCodeMaxLength);
            Check.NotNullOrWhiteSpace(successEvent, nameof(successEvent));
            Check.Length(successEvent, nameof(successEvent), RuleConsts.SuccessEventMaxLength);
            Check.NotNullOrWhiteSpace(errorMessage, nameof(errorMessage));
            Check.Length(errorMessage, nameof(errorMessage), RuleConsts.ErrorMessageMaxLength);
            Check.NotNull(errorType, nameof(errorType));
            Check.NotNull(ruleExpressionType, nameof(ruleExpressionType));

            var rule = new Rule(
             GuidGenerator.Create(),
             ruleCode, successEvent, errorMessage, errorType, ruleExpressionType, expression, description
             );

            return await _ruleRepository.InsertAsync(rule);
        }

        public virtual async Task<Rule> UpdateAsync(
            Guid id,
            string ruleCode, string successEvent, string errorMessage, ErrorType errorType, RuleExpressionType ruleExpressionType, string expression, string description, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(ruleCode, nameof(ruleCode));
            Check.Length(ruleCode, nameof(ruleCode), RuleConsts.RuleCodeMaxLength);
            Check.NotNullOrWhiteSpace(successEvent, nameof(successEvent));
            Check.Length(successEvent, nameof(successEvent), RuleConsts.SuccessEventMaxLength);
            Check.NotNullOrWhiteSpace(errorMessage, nameof(errorMessage));
            Check.Length(errorMessage, nameof(errorMessage), RuleConsts.ErrorMessageMaxLength);
            Check.NotNull(errorType, nameof(errorType));
            Check.NotNull(ruleExpressionType, nameof(ruleExpressionType));

            var rule = await _ruleRepository.GetAsync(id);

            rule.RuleCode = ruleCode;
            rule.SuccessEvent = successEvent;
            rule.ErrorMessage = errorMessage;
            rule.ErrorType = errorType;
            rule.RuleExpressionType = ruleExpressionType;
            rule.Expression = expression;
            rule.Description = description;

            rule.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _ruleRepository.UpdateAsync(rule);
        }

    }
}