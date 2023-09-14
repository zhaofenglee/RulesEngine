using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public class RulesMember : RulesMemberBase
    {
       
        public RulesMember()
        {

        }

        public RulesMember(Guid id, Guid? rulesGroupId, Guid? ruleId, int sequence, string description)
            : base(id, rulesGroupId, ruleId, sequence, description)
        {
        }
        
    }
}