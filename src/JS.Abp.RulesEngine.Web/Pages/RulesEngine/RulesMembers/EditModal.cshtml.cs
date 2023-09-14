using JS.Abp.RulesEngine.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using JS.Abp.RulesEngine.RulesMembers;

namespace JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesMembers
{
    public abstract class EditModalModelBase : RulesEnginePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public RulesMemberUpdateViewModel RulesMember { get; set; }

        public List<SelectListItem> RulesGroupLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };
        public List<SelectListItem> RuleLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        protected IRulesMembersAppService _rulesMembersAppService;

        public EditModalModelBase(IRulesMembersAppService rulesMembersAppService)
        {
            _rulesMembersAppService = rulesMembersAppService;

            RulesMember = new();
        }

        public virtual async Task OnGetAsync()
        {
            var rulesMemberWithNavigationPropertiesDto = await _rulesMembersAppService.GetWithNavigationPropertiesAsync(Id);
            RulesMember = ObjectMapper.Map<RulesMemberDto, RulesMemberUpdateViewModel>(rulesMemberWithNavigationPropertiesDto.RulesMember);

            RulesGroupLookupList.AddRange((
                                    await _rulesMembersAppService.GetRulesGroupLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );
            RuleLookupList.AddRange((
                                    await _rulesMembersAppService.GetRuleLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _rulesMembersAppService.UpdateAsync(Id, ObjectMapper.Map<RulesMemberUpdateViewModel, RulesMemberUpdateDto>(RulesMember));
            return NoContent();
        }
    }

    public class RulesMemberUpdateViewModel : RulesMemberUpdateDto
    {
    }
}