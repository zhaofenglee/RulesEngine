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
    public class CreateModalModel : CreateModalModelBase
    {
        public CreateModalModel(IRulesMembersAppService rulesMembersAppService)
            : base(rulesMembersAppService)
        {
        }
    }
}