using JS.Abp.RulesEngine.OperatorTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesGroups
{
    public abstract class IndexModelBase : AbpPageModel
    {
        public string? GroupNameFilter { get; set; }
        public OperatorType? OperatorTypeFilter { get; set; }
        public string? DescriptionFilter { get; set; }

        protected IRulesGroupsAppService _rulesGroupsAppService;

        public IndexModelBase(IRulesGroupsAppService rulesGroupsAppService)
        {
            _rulesGroupsAppService = rulesGroupsAppService;
        }

        public virtual async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}