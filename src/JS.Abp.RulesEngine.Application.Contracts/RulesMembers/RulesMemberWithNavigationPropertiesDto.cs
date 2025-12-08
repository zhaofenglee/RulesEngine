using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace JS.Abp.RulesEngine.RulesMembers
{
#pragma warning disable CS8618
    public abstract class RulesMemberWithNavigationPropertiesDtoBase
    {
        public RulesMemberDto RulesMember { get; set; }

        public RulesGroupDto? RulesGroup { get; set; }
        public RuleDto? Rule { get; set; }

    }
#pragma warning restore CS8618
}