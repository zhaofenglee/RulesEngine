using Volo.Abp.Application.Dtos;
using System;

namespace JS.Abp.RulesEngine.RulesMembers
{
#pragma warning disable CS8618
    public abstract class RulesMemberExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public int? SequenceMin { get; set; }
        public int? SequenceMax { get; set; }
        public string? Description { get; set; }
        public Guid? RulesGroupId { get; set; }
        public Guid? RuleId { get; set; }

        public RulesMemberExcelDownloadDtoBase()
        {

        }
    }
#pragma warning restore CS8618
}