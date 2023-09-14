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
    public class EditModalModel : EditModalModelBase
    {
        public EditModalModel(IRulesGroupsAppService rulesGroupsAppService)
            : base(rulesGroupsAppService)
        {
        }
    }
}