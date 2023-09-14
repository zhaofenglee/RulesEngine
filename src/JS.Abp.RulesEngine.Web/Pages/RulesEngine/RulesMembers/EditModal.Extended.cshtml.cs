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
    public class EditModalModel : EditModalModelBase
    {
        public EditModalModel(IRulesMembersAppService rulesMembersAppService)
            : base(rulesMembersAppService)
        {
        }
    }
}