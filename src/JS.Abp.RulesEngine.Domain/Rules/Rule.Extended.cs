using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using JS.Abp.RulesEngine.ErrorTypes;
using JS.Abp.RulesEngine.RuleExpressionTypes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace JS.Abp.RulesEngine.Rules
{
    public class Rule : RuleBase
    {
       
        public Rule()
        {

        }

        public Rule(Guid id, string ruleCode, string successEvent, string errorMessage, ErrorType errorType, RuleExpressionType ruleExpressionType, string expression, string description)
            : base(id, ruleCode, successEvent, errorMessage, errorType, ruleExpressionType, expression, description)
        {
        }
        
    }
}