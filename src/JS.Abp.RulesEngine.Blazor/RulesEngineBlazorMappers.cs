using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.RulesMembers;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace JS.Abp.RulesEngine.Blazor;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RuleDtoToRuleUpdateDtoMapper : MapperBase<RuleDto, RuleUpdateDto>
{
    public override partial RuleUpdateDto Map(RuleDto source);

    public override partial void Map(RuleDto source, RuleUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesGroupDtoToRulesGroupUpdateDtoMapper : MapperBase<RulesGroupDto, RulesGroupUpdateDto>
{
    public override partial RulesGroupUpdateDto Map(RulesGroupDto source);

    public override partial void Map(RulesGroupDto source, RulesGroupUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesMemberDtoToRulesMemberUpdateDtoMapper : MapperBase<RulesMemberDto, RulesMemberUpdateDto>
{
    public override partial RulesMemberUpdateDto Map(RulesMemberDto source);

    public override partial void Map(RulesMemberDto source, RulesMemberUpdateDto destination);
}
