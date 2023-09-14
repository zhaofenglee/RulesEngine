using JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Web.Pages.RulesEngine.Rules;
using Volo.Abp.AutoMapper;
using JS.Abp.RulesEngine.Rules;
using AutoMapper;

namespace JS.Abp.RulesEngine.Web;

public class RulesEngineWebAutoMapperProfile : Profile
{
    public RulesEngineWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<RuleDto, RuleUpdateViewModel>();
        CreateMap<RuleUpdateViewModel, RuleUpdateDto>();
        CreateMap<RuleCreateViewModel, RuleCreateDto>();

        CreateMap<RulesGroupDto, RulesGroupUpdateViewModel>();
        CreateMap<RulesGroupUpdateViewModel, RulesGroupUpdateDto>();
        CreateMap<RulesGroupCreateViewModel, RulesGroupCreateDto>();

        CreateMap<RulesMemberDto, RulesMemberUpdateViewModel>();
        CreateMap<RulesMemberUpdateViewModel, RulesMemberUpdateDto>();
        CreateMap<RulesMemberCreateViewModel, RulesMemberCreateDto>();
    }
}