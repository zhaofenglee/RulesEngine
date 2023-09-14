using JS.Abp.RulesEngine.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using JS.Abp.RulesEngine.RulesGroups;

namespace JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesGroups
{
    public abstract class EditModalModelBase : RulesEnginePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public RulesGroupUpdateViewModel RulesGroup { get; set; }

        protected IRulesGroupsAppService _rulesGroupsAppService;

        public EditModalModelBase(IRulesGroupsAppService rulesGroupsAppService)
        {
            _rulesGroupsAppService = rulesGroupsAppService;

            RulesGroup = new();
        }

        public virtual async Task OnGetAsync()
        {
            var rulesGroup = await _rulesGroupsAppService.GetAsync(Id);
            RulesGroup = ObjectMapper.Map<RulesGroupDto, RulesGroupUpdateViewModel>(rulesGroup);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _rulesGroupsAppService.UpdateAsync(Id, ObjectMapper.Map<RulesGroupUpdateViewModel, RulesGroupUpdateDto>(RulesGroup));
            return NoContent();
        }
    }

    public class RulesGroupUpdateViewModel : RulesGroupUpdateDto
    {
    }
}