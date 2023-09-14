using JS.Abp.RulesEngine.OperatorTypes;
using JS.Abp.RulesEngine.OperatorTypes;
using JS.Abp.RulesEngine.OperatorTypes;
using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.ErrorTypes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace JS.Abp.RulesEngine.RulesGroups
{
    public class RulesGroup : RulesGroupBase
    {
      
        public RulesGroup()
        {

        }

        public RulesGroup(Guid id, string groupName, OperatorType operatorType, string description)
            : base(id, groupName, operatorType, description)
        {
        }
      
    }
}