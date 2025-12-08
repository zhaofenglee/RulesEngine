using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.Web.Pages.RulesEngine.Rules;
using JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.Web.Pages.RulesEngine.RulesMembers;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace JS.Abp.RulesEngine.Web;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RuleDtoToRuleUpdateViewModelMapper : MapperBase<RuleDto, RuleUpdateViewModel>
{
    public override partial RuleUpdateViewModel Map(RuleDto source);

    public override partial void Map(RuleDto source, RuleUpdateViewModel destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RuleUpdateViewModelToRuleUpdateDtoMapper : MapperBase<RuleUpdateViewModel, RuleUpdateDto>
{
    public override partial RuleUpdateDto Map(RuleUpdateViewModel source);

    public override partial void Map(RuleUpdateViewModel source, RuleUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RuleCreateViewModelToRuleCreateDtoMapper : MapperBase<RuleCreateViewModel, RuleCreateDto>
{
    public override partial RuleCreateDto Map(RuleCreateViewModel source);

    public override partial void Map(RuleCreateViewModel source, RuleCreateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesGroupDtoToRulesGroupUpdateViewModelMapper : MapperBase<RulesGroupDto, RulesGroupUpdateViewModel>
{
    public override partial RulesGroupUpdateViewModel Map(RulesGroupDto source);

    public override partial void Map(RulesGroupDto source, RulesGroupUpdateViewModel destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesGroupUpdateViewModelToRulesGroupUpdateDtoMapper : MapperBase<RulesGroupUpdateViewModel, RulesGroupUpdateDto>
{
    public override partial RulesGroupUpdateDto Map(RulesGroupUpdateViewModel source);

    public override partial void Map(RulesGroupUpdateViewModel source, RulesGroupUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesGroupCreateViewModelToRulesGroupCreateDtoMapper : MapperBase<RulesGroupCreateViewModel, RulesGroupCreateDto>
{
    public override partial RulesGroupCreateDto Map(RulesGroupCreateViewModel source);

    public override partial void Map(RulesGroupCreateViewModel source, RulesGroupCreateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesMemberDtoToRulesMemberUpdateViewModelMapper : MapperBase<RulesMemberDto, RulesMemberUpdateViewModel>
{
    public override partial RulesMemberUpdateViewModel Map(RulesMemberDto source);

    public override partial void Map(RulesMemberDto source, RulesMemberUpdateViewModel destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesMemberUpdateViewModelToRulesMemberUpdateDtoMapper : MapperBase<RulesMemberUpdateViewModel, RulesMemberUpdateDto>
{
    public override partial RulesMemberUpdateDto Map(RulesMemberUpdateViewModel source);

    public override partial void Map(RulesMemberUpdateViewModel source, RulesMemberUpdateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class RulesMemberCreateViewModelToRulesMemberCreateDtoMapper : MapperBase<RulesMemberCreateViewModel, RulesMemberCreateDto>
{
    public override partial RulesMemberCreateDto Map(RulesMemberCreateViewModel source);

    public override partial void Map(RulesMemberCreateViewModel source, RulesMemberCreateDto destination);
}

