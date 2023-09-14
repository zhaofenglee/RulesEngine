using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using Volo.Abp.Application.Dtos;
using System;

namespace JS.Abp.RulesEngine.Rules
{
#pragma warning disable CS8618
    public abstract class GetRulesInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? RuleCode { get; set; }
        public string? SuccessEvent { get; set; }
        public string? ErrorMessage { get; set; }
        public ErrorType? ErrorType { get; set; }
        public RuleExpressionType? RuleExpressionType { get; set; }
        public string? Expression { get; set; }
        public string? Description { get; set; }

        public GetRulesInputBase()
        {

        }
    }
#pragma warning restore CS8618
}