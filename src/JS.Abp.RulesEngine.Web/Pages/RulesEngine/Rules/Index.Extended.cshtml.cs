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
    public class IndexModel : IndexModelBase
    {
        public IndexModel(IRulesAppService rulesAppService)
            : base(rulesAppService)
        {
        }
    }
}