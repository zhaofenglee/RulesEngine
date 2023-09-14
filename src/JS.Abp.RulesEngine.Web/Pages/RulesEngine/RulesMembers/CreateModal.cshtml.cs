using JS.Abp.RulesEngine.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JS.Abp.RulesEngine.RulesMembers;

namespace JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesMembers
{
    public abstract class CreateModalModelBase : RulesEnginePageModel
    {
        [BindProperty(SupportsGet = true)] 
        public Guid RulesGroupId { get; set; }
        [BindProperty]
        public RulesMemberCreateViewModel RulesMember { get; set; }

        // public List<SelectListItem> RulesGroupLookupList { get; set; } = new List<SelectListItem>
        // {
        //     new SelectListItem(" — ", "")
        // };
        public List<SelectListItem> RuleLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(" — ", "")
        };

        protected IRulesMembersAppService _rulesMembersAppService;

        public CreateModalModelBase(IRulesMembersAppService rulesMembersAppService)
        {
            _rulesMembersAppService = rulesMembersAppService;

            RulesMember = new()
            {
                RulesGroupId = RulesGroupId,
            };
        }

        public virtual async Task OnGetAsync()
        {
            RulesMember = new RulesMemberCreateViewModel()
            {
                RulesGroupId = RulesGroupId,
            };
            // RulesGroupLookupList.AddRange((
            //                         await _rulesMembersAppService.GetRulesGroupLookupAsync(new LookupRequestDto
            //                         {
            //                             MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
            //                         })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
            //             );
            RuleLookupList.AddRange((
                                    await _rulesMembersAppService.GetRuleLookupAsync(new LookupRequestDto
                                    {
                                        MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
                                    })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList()
                        );

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _rulesMembersAppService.CreateAsync(ObjectMapper.Map<RulesMemberCreateViewModel, RulesMemberCreateDto>(RulesMember));
            return NoContent();
        }
    }

    public class RulesMemberCreateViewModel : RulesMemberCreateDto
    {
    }
}