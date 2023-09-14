using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.RulesEngine.Rules
{
#pragma warning disable CS8618
    public abstract class RuleUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(RuleConsts.RuleCodeMaxLength)]
        public string RuleCode { get; set; }
        [Required]
        [StringLength(RuleConsts.SuccessEventMaxLength)]
        public string SuccessEvent { get; set; }
        [Required]
        [StringLength(RuleConsts.ErrorMessageMaxLength)]
        public string ErrorMessage { get; set; }
        public ErrorType ErrorType { get; set; }
        public RuleExpressionType RuleExpressionType { get; set; }
        public string? Expression { get; set; }
        public string? Description { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
#pragma warning restore CS8618
}