using JS.Abp.RulesEngine.OperatorTypes;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace JS.Abp.RulesEngine.RulesGroups
{
#pragma warning disable CS8618
    public abstract class RulesGroupDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string GroupName { get; set; }
        public OperatorType OperatorType { get; set; }
        public string? Description { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
#pragma warning restore CS8618
}