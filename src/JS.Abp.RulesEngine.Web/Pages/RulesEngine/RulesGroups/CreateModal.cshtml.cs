using JS.Abp.RulesEngine.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JS.Abp.RulesEngine.RulesGroups;

namespace JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesGroups
{
    public abstract class CreateModalModelBase : RulesEnginePageModel
    {
        [BindProperty]
        public RulesGroupCreateViewModel RulesGroup { get; set; }

        protected IRulesGroupsAppService _rulesGroupsAppService;

        public CreateModalModelBase(IRulesGroupsAppService rulesGroupsAppService)
        {
            _rulesGroupsAppService = rulesGroupsAppService;

            RulesGroup = new();
        }

        public virtual async Task OnGetAsync()
        {
            RulesGroup = new RulesGroupCreateViewModel();

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _rulesGroupsAppService.CreateAsync(ObjectMapper.Map<RulesGroupCreateViewModel, RulesGroupCreateDto>(RulesGroup));
            return NoContent();
        }
    }

    public class RulesGroupCreateViewModel : RulesGroupCreateDto
    {
    }
}