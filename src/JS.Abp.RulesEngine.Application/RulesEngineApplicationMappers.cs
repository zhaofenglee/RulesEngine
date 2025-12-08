using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.RulesGroups;
using System;
using JS.Abp.RulesEngine.Shared;
using JS.Abp.RulesEngine.Rules;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace JS.Abp.RulesEngine;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RuleToRuleDtoMapper : MapperBase<Rule, RuleDto>
{
    public override partial RuleDto Map(Rule source);
    public override partial void Map(Rule source, RuleDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RuleToRuleExcelDtoMapper : MapperBase<Rule, RuleExcelDto>
{
    public override partial RuleExcelDto Map(Rule source);
    public override partial void Map(Rule source, RuleExcelDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesGroupToRulesGroupDtoMapper : MapperBase<RulesGroup, RulesGroupDto>
{
    public override partial RulesGroupDto Map(RulesGroup source);
    public override partial void Map(RulesGroup source, RulesGroupDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesGroupToRulesGroupExcelDtoMapper : MapperBase<RulesGroup, RulesGroupExcelDto>
{
    public override partial RulesGroupExcelDto Map(RulesGroup source);
    public override partial void Map(RulesGroup source, RulesGroupExcelDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesMemberToRulesMemberDtoMapper : MapperBase<RulesMember, RulesMemberDto>
{
    public override partial RulesMemberDto Map(RulesMember source);
    public override partial void Map(RulesMember source, RulesMemberDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesMemberToRulesMemberExcelDtoMapper : MapperBase<RulesMember, RulesMemberExcelDto>
{
    public override partial RulesMemberExcelDto Map(RulesMember source);
    public override partial void Map(RulesMember source, RulesMemberExcelDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesMemberWithNavigationPropertiesToRulesMemberWithNavigationPropertiesDtoMapper : MapperBase<RulesMemberWithNavigationProperties, RulesMemberWithNavigationPropertiesDto>
{
    public override partial RulesMemberWithNavigationPropertiesDto Map(RulesMemberWithNavigationProperties source);
    public override partial void Map(RulesMemberWithNavigationProperties source, RulesMemberWithNavigationPropertiesDto destination);
    
    private partial RulesGroupDto? MapToRulesGroupDto(RulesGroup? source);
    private partial RuleDto? MapToRuleDto(Rule? source);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesGroupToLookupDtoMapper : MapperBase<RulesGroup, LookupDto<Guid>>
{
    [MapProperty(nameof(RulesGroup.GroupName), nameof(LookupDto<Guid>.DisplayName))]
    public override partial LookupDto<Guid> Map(RulesGroup source);
    
    [MapProperty(nameof(RulesGroup.GroupName), nameof(LookupDto<Guid>.DisplayName))]
    public override partial void Map(RulesGroup source, LookupDto<Guid> destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RuleToLookupDtoMapper : MapperBase<Rule, LookupDto<Guid>>
{
    [MapProperty(nameof(Rule.RuleCode), nameof(LookupDto<Guid>.DisplayName))]
    public override partial LookupDto<Guid> Map(Rule source);
    
    [MapProperty(nameof(Rule.RuleCode), nameof(LookupDto<Guid>.DisplayName))]
    public override partial void Map(Rule source, LookupDto<Guid> destination);
}

