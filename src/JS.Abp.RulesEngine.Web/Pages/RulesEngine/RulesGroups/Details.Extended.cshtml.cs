using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JS.Abp.RulesEngine.RulesGroups;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.Shared;

namespace JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesGroups
{
    public class DetailsModel : DetailsModelBase
    {
        public DetailsModel(IRulesMembersAppService rulesMembersAppService, IRulesGroupsAppService rulesGroupsAppService)
            : base(rulesMembersAppService,rulesGroupsAppService)
        {
        }
    }
}