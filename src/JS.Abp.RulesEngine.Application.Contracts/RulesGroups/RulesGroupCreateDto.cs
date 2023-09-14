using JS.Abp.RulesEngine.OperatorTypes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JS.Abp.RulesEngine.RulesGroups
{
#pragma warning disable CS8618
    public abstract class RulesGroupCreateDtoBase
    {
        [Required]
        [StringLength(RulesGroupConsts.GroupNameMaxLength)]
        public string GroupName { get; set; }
        public OperatorType OperatorType { get; set; } = ((OperatorType[])Enum.GetValues(typeof(OperatorType)))[0];
        public string? Description { get; set; }
    }
#pragma warning restore CS8618
}