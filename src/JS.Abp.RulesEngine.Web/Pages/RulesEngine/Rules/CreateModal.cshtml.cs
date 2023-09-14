using JS.Abp.RulesEngine.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JS.Abp.RulesEngine.Rules;

namespace JS.Abp.RulesEngine.Web.Pages.RulesEngine.Rules
{
    public abstract class CreateModalModelBase : RulesEnginePageModel
    {
        [BindProperty]
        public RuleCreateViewModel Rule { get; set; }

        protected IRulesAppService _rulesAppService;

        public CreateModalModelBase(IRulesAppService rulesAppService)
        {
            _rulesAppService = rulesAppService;

            Rule = new();
        }

        public virtual async Task OnGetAsync()
        {
            Rule = new RuleCreateViewModel();

            await Task.CompletedTask;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {

            await _rulesAppService.CreateAsync(ObjectMapper.Map<RuleCreateViewModel, RuleCreateDto>(Rule));
            return NoContent();
        }
    }

    public class RuleCreateViewModel : RuleCreateDto
    {
    }
}