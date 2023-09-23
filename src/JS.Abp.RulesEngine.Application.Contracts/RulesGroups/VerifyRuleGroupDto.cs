using System.ComponentModel.DataAnnotations;
using Volo.Abp.Data;

namespace JS.Abp.RulesEngine.RulesGroups;

public class VerifyRuleGroupDto:IHasExtraProperties
{
    [Required]
    public string GroupName { get; set; }
    
    public ExtraPropertyDictionary ExtraProperties { get;set; } = new ExtraPropertyDictionary();
    
    public VerifyRuleGroupDto()
    {
        
    }
}
   