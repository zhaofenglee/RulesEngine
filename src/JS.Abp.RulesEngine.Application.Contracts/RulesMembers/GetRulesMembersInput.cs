using Volo.Abp.Application.Dtos;
using System;

namespace JS.Abp.RulesEngine.RulesMembers
{
#pragma warning disable CS8618
    public abstract class GetRulesMembersInputBase : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public int? SequenceMin { get; set; }
        public int? SequenceMax { get; set; }
        public string? Description { get; set; }
        public Guid? RulesGroupId { get; set; }
        public Guid? RuleId { get; set; }

        public GetRulesMembersInputBase()
        {

        }
    }
#pragma warning restore CS8618
}