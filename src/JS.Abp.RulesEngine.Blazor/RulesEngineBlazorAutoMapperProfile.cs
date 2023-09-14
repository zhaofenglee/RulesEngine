using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesGroups;
using Volo.Abp.AutoMapper;
using JS.Abp.RulesEngine.Rules;
using AutoMapper;

namespace JS.Abp.RulesEngine.Blazor;

public class RulesEngineBlazorAutoMapperProfile : Profile
{
    public RulesEngineBlazorAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<RuleDto, RuleUpdateDto>();

        CreateMap<RulesGroupDto, RulesGroupUpdateDto>();

        CreateMap<RulesMemberDto, RulesMemberUpdateDto>();
        
    }
}