using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesGroups;
using System;
using JS.Abp.RulesEngine.Shared;
using Volo.Abp.AutoMapper;
using JS.Abp.RulesEngine.Rules;
using AutoMapper;

namespace JS.Abp.RulesEngine;

public class RulesEngineApplicationAutoMapperProfile : Profile
{
    public RulesEngineApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Rule, RuleDto>();
        CreateMap<Rule, RuleExcelDto>();

        CreateMap<RulesGroup, RulesGroupDto>();
        CreateMap<RulesGroup, RulesGroupExcelDto>();

        CreateMap<RulesMember, RulesMemberDto>();
        CreateMap<RulesMember, RulesMemberExcelDto>();
        CreateMap<RulesMemberWithNavigationProperties, RulesMemberWithNavigationPropertiesDto>();
        CreateMap<RulesGroup, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.GroupName));

        CreateMap<Rule, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.RuleCode));

        
    }
}