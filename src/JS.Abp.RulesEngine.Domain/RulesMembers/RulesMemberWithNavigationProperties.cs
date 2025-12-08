using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;

using System;
using System.Collections.Generic;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public abstract class RulesMemberWithNavigationPropertiesBase
    {
        public RulesMember RulesMember { get; set; }

        public RulesGroup? RulesGroup { get; set; }
        public Rule? Rule { get; set; }
        

        
    }
}