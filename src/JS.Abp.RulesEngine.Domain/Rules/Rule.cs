using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace JS.Abp.RulesEngine.Rules
{
    public abstract class RuleBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string RuleCode { get; set; }

        [NotNull]
        public virtual string SuccessEvent { get; set; }

        [NotNull]
        public virtual string ErrorMessage { get; set; }

        public virtual ErrorType ErrorType { get; set; }

        public virtual RuleExpressionType RuleExpressionType { get; set; }

        [CanBeNull]
        public virtual string? Expression { get; set; }

        [CanBeNull]
        public virtual string? Description { get; set; }

        public RuleBase()
        {

        }

        public RuleBase(Guid id, string ruleCode, string successEvent, string errorMessage, ErrorType errorType, RuleExpressionType ruleExpressionType, string expression, string description)
        {

            Id = id;
            Check.NotNull(ruleCode, nameof(ruleCode));
            Check.Length(ruleCode, nameof(ruleCode), RuleConsts.RuleCodeMaxLength, 0);
            Check.NotNull(successEvent, nameof(successEvent));
            Check.Length(successEvent, nameof(successEvent), RuleConsts.SuccessEventMaxLength, 0);
            Check.NotNull(errorMessage, nameof(errorMessage));
            Check.Length(errorMessage, nameof(errorMessage), RuleConsts.ErrorMessageMaxLength, 0);
            RuleCode = ruleCode;
            SuccessEvent = successEvent;
            ErrorMessage = errorMessage;
            ErrorType = errorType;
            RuleExpressionType = ruleExpressionType;
            Expression = expression;
            Description = description;
        }

    }
}