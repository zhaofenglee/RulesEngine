using JS.Abp.RulesEngine.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using JS.Abp.RulesEngine.Rules;

namespace JS.Abp.RulesEngine.Web.Pages.RulesEngine.Rules
{
    public abstract class EditModalModelBase : RulesEnginePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public RuleUpdateViewModel Rule { get; set; }

        protected IRulesAppService _rulesAppService;

        public EditModalModelBase(IRulesAppService rulesAppService)
        {
            _rulesAppService = rulesAppService;

            Rule = new();
        }

        public virtual async Task OnGetAsync()
        {
            var rule = await _rulesAppService.GetAsync(Id);
            Rule = ObjectMapper.Map<RuleDto, RuleUpdateViewModel>(rule);

        }

        public virtual async Task<NoContentResult> OnPostAsync()
        {

            await _rulesAppService.UpdateAsync(Id, ObjectMapper.Map<RuleUpdateViewModel, RuleUpdateDto>(Rule));
            return NoContent();
        }
    }

    public class RuleUpdateViewModel : RuleUpdateDto
    {
    }
}