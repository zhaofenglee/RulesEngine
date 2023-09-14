using JS.Abp.RulesEngine.OperatorTypes;
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
    public abstract class RulesGroupBase : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string GroupName { get; set; }

        public virtual OperatorType OperatorType { get; set; }

        [CanBeNull]
        public virtual string? Description { get; set; }

        public RulesGroupBase()
        {

        }

        public RulesGroupBase(Guid id, string groupName, OperatorType operatorType, string description)
        {

            Id = id;
            Check.NotNull(groupName, nameof(groupName));
            Check.Length(groupName, nameof(groupName), RulesGroupConsts.GroupNameMaxLength, 0);
            GroupName = groupName;
            OperatorType = operatorType;
            Description = description;
        }

    }
}