using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.RulesEngine.Rules
{
#pragma warning disable CS8618
    public abstract class RuleDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string RuleCode { get; set; }
        public string SuccessEvent { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorType ErrorType { get; set; }
        public RuleExpressionType RuleExpressionType { get; set; }
        public string? Expression { get; set; }
        public string? Description { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
#pragma warning restore CS8618
}