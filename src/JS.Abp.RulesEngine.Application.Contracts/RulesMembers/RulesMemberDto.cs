using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.RulesEngine.RulesMembers
{
#pragma warning disable CS8618
    public abstract class RulesMemberDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public int Sequence { get; set; }
        public string? Description { get; set; }
        public Guid? RulesGroupId { get; set; }
        public Guid? RuleId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
#pragma warning restore CS8618
}