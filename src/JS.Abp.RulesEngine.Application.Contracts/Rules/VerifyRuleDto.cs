using System.ComponentModel.DataAnnotations;
using System.Linq;
using Volo.Abp.Data;

namespace JS.Abp.RulesEngine.Rules;

public class VerifyRuleDto:IHasExtraProperties
{
    [Required]
    public string RuleCode { get; set; }
    
    public ExtraPropertyDictionary ExtraProperties { get;set; } = new ExtraPropertyDictionary();
    
    public VerifyRuleDto()
    {
        
    }
}