using JS.Abp.RulesEngine.OperatorTypes;
using System;

namespace JS.Abp.RulesEngine.RulesGroups
{
#pragma warning disable CS8618
    public abstract class RulesGroupExcelDtoBase
    {
        public string GroupName { get; set; }
        public OperatorType OperatorType { get; set; }
        public string? Description { get; set; }
    }
#pragma warning restore CS8618
}