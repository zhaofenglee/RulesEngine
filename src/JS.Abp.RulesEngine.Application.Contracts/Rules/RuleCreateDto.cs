using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JS.Abp.RulesEngine.Rules
{
#pragma warning disable CS8618
    public abstract class RuleCreateDtoBase
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
        public ErrorType ErrorType { get; set; } = ((ErrorType[])Enum.GetValues(typeof(ErrorType)))[0];
        public RuleExpressionType RuleExpressionType { get; set; } = ((RuleExpressionType[])Enum.GetValues(typeof(RuleExpressionType)))[0];
        public string? Expression { get; set; }
        public string? Description { get; set; }
    }
#pragma warning restore CS8618
}