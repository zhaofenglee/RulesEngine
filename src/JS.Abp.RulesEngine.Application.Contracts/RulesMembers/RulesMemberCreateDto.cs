using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JS.Abp.RulesEngine.RulesMembers
{
#pragma warning disable CS8618
    public abstract class RulesMemberCreateDtoBase
    {
        public int Sequence { get; set; }
        public string? Description { get; set; }
        public Guid? RulesGroupId { get; set; }
        public Guid? RuleId { get; set; }
    }
#pragma warning restore CS8618
}