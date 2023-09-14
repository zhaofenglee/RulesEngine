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
using Microsoft.AspNetCore.Mvc;

namespace JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesGroups
{
    public abstract class DetailsModelBase : AbpPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid RulesGroupId { get; set; }
        public int? SequenceFilterMin { get; set; }

        public int? SequenceFilterMax { get; set; }
        public string? DescriptionFilter { get; set; }
        [SelectItems(nameof(RulesGroupLookupList))]
        public Guid? RulesGroupIdFilter { get; set; }
        public List<SelectListItem> RulesGroupLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        [SelectItems(nameof(RuleLookupList))]
        public Guid? RuleIdFilter { get; set; }
        public List<SelectListItem> RuleLookupList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem(string.Empty, "")
        };

        protected IRulesMembersAppService _rulesMembersAppService;
        protected IRulesGroupsAppService _rulesGroupsAppService;
        [BindProperty]
        public RulesGroupUpdateViewModel RulesGroup { get; set; }
        public DetailsModelBase(IRulesMembersAppService rulesMembersAppService, IRulesGroupsAppService rulesGroupsAppService)
        {
            _rulesMembersAppService = rulesMembersAppService;
            _rulesGroupsAppService = rulesGroupsAppService;
            RulesGroup = new();
        }

        public virtual async Task OnGetAsync()
        {
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
            var rulesGroup = await _rulesGroupsAppService.GetAsync(RulesGroupId);
            RulesGroup = ObjectMapper.Map<RulesGroupDto, RulesGroupUpdateViewModel>(rulesGroup);
            await Task.CompletedTask;
        }
    }
}