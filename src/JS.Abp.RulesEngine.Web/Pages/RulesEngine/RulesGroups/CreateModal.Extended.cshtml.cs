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
    public class CreateModalModel : CreateModalModelBase
    {
        public CreateModalModel(IRulesGroupsAppService rulesGroupsAppService)
            : base(rulesGroupsAppService)
        {
        }
    }
}