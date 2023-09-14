using JS.Abp.RulesEngine.RuleExpressionTypes;
using JS.Abp.RulesEngine.ErrorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.Web.Pages.RulesEngine.Rules
{
    public abstract class IndexModelBase : AbpPageModel
    {
        public string? RuleCodeFilter { get; set; }
        public string? SuccessEventFilter { get; set; }
        public string? ErrorMessageFilter { get; set; }
        public ErrorType? ErrorTypeFilter { get; set; }
        public RuleExpressionType? RuleExpressionTypeFilter { get; set; }
        public string? ExpressionFilter { get; set; }
        public string? DescriptionFilter { get; set; }

        protected IRulesAppService _rulesAppService;

        public IndexModelBase(IRulesAppService rulesAppService)
        {
            _rulesAppService = rulesAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}