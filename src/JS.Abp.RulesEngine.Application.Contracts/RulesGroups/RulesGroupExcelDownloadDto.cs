using JS.Abp.RulesEngine.OperatorTypes;
using Volo.Abp.Application.Dtos;
using System;

namespace JS.Abp.RulesEngine.RulesGroups
{
#pragma warning disable CS8618
    public abstract class RulesGroupExcelDownloadDtoBase
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? GroupName { get; set; }
        public OperatorType? OperatorType { get; set; }
        public string? Description { get; set; }

        public RulesGroupExcelDownloadDtoBase()
        {

        }
    }
#pragma warning restore CS8618
}