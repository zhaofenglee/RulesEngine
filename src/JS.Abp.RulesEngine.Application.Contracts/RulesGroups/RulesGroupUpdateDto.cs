using JS.Abp.RulesEngine.OperatorTypes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.RulesEngine.RulesGroups
{
#pragma warning disable CS8618
    public abstract class RulesGroupUpdateDtoBase : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(RulesGroupConsts.GroupNameMaxLength)]
        public string GroupName { get; set; }
        public OperatorType OperatorType { get; set; }
        public string? Description { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
#pragma warning restore CS8618
}