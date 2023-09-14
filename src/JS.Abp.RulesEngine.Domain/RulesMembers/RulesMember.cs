using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Rules;
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
    public abstract class RulesMemberBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        public virtual int Sequence { get; set; }

        [CanBeNull]
        public virtual string? Description { get; set; }
        public Guid? RulesGroupId { get; set; }
        public Guid? RuleId { get; set; }

        public RulesMemberBase()
        {

        }

        public RulesMemberBase(Guid id, Guid? rulesGroupId, Guid? ruleId, int sequence, string description)
        {

            Id = id;
            Sequence = sequence;
            Description = description;
            RulesGroupId = rulesGroupId;
            RuleId = ruleId;
        }

    }
}