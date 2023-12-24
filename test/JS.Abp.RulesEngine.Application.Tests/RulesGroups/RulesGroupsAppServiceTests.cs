using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using JS.Abp.RulesEngine.OperatorTypes;
using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.Stores;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Modularity;
using Xunit;

namespace JS.Abp.RulesEngine.RulesGroups
{
    public abstract class RulesGroupsAppServiceTests <TStartupModule> : RulesEngineApplicationTestBase<TStartupModule>
        where TStartupModule : IAbpModule
    {
        private readonly IRulesGroupsAppService _rulesGroupsAppService;
        private readonly IRepository<RulesGroup, Guid> _rulesGroupRepository;
        private readonly IRepository<Rule, Guid> _ruleRepository;
        private readonly IRepository<RulesMember, Guid> _rulesMemberRepository;
        public RulesGroupsAppServiceTests()
        {
            _rulesGroupsAppService = GetRequiredService<IRulesGroupsAppService>();
            _rulesGroupRepository = GetRequiredService<IRepository<RulesGroup, Guid>>();
            _ruleRepository = GetRequiredService<IRepository<Rule, Guid>>();
            _rulesMemberRepository = GetRequiredService<IRepository<RulesMember, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _rulesGroupsAppService.GetListAsync(new GetRulesGroupsInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("15d16d9c-bb47-40e7-ad2d-60075e6c8b11")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("8e6af1ef-07c5-493f-b727-d5b2ac81c10c")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _rulesGroupsAppService.GetAsync(Guid.Parse("15d16d9c-bb47-40e7-ad2d-60075e6c8b11"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("15d16d9c-bb47-40e7-ad2d-60075e6c8b11"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new RulesGroupCreateDto
            {
                GroupName = "401197a0fdc94b0bae7b2c5168bdea220644c4a1629a476fbd0c5f10a8ab88fec641d204b8ee481ca53ea152db126f764e88b915229340f5ac3d26de1eb609e0",
                OperatorType = default,
                Description = "0c70270feedb48"
            };

            // Act
            var serviceResult = await _rulesGroupsAppService.CreateAsync(input);

            // Assert
            var result = await _rulesGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.GroupName.ShouldBe("401197a0fdc94b0bae7b2c5168bdea220644c4a1629a476fbd0c5f10a8ab88fec641d204b8ee481ca53ea152db126f764e88b915229340f5ac3d26de1eb609e0");
            result.OperatorType.ShouldBe(default);
            result.Description.ShouldBe("0c70270feedb48");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new RulesGroupUpdateDto()
            {
                GroupName = "d2cde8c2bd6048cb89165091458de510f69a9744f277444bb9a861f64418ab1756361abde84c4088bf0f39104240fa80cbe85af6ea264181a65dc7063a139825",
                OperatorType = default,
                Description = "d6ff027"
            };

            // Act
            var serviceResult = await _rulesGroupsAppService.UpdateAsync(Guid.Parse("15d16d9c-bb47-40e7-ad2d-60075e6c8b11"), input);

            // Assert
            var result = await _rulesGroupRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.GroupName.ShouldBe("d2cde8c2bd6048cb89165091458de510f69a9744f277444bb9a861f64418ab1756361abde84c4088bf0f39104240fa80cbe85af6ea264181a65dc7063a139825");
            result.OperatorType.ShouldBe(default);
            result.Description.ShouldBe("d6ff027");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _rulesGroupsAppService.DeleteAsync(Guid.Parse("15d16d9c-bb47-40e7-ad2d-60075e6c8b11"));

            // Assert
            var result = await _rulesGroupRepository.FindAsync(c => c.Id == Guid.Parse("15d16d9c-bb47-40e7-ad2d-60075e6c8b11"));

            result.ShouldBeNull();
        }

        [Fact]
        public async Task VerifyRulesGroupAsync()
        {
            // Arrange
        var input1 = new RulesGroup()
        {
            GroupName = "TestGroup-Or",
            OperatorType = OperatorType.Or,
          
            Description = "168ca455c"
        };
        var input2 = new RulesGroup()
        {
            GroupName = "TestGroupAnd",
            OperatorType = OperatorType.And,
           
            Description = "168ca455c"
        };
        // Act
        var rulesGroupResult1 = await _rulesGroupRepository.InsertAsync(input1);
        var rulesGroupResult2 = await _rulesGroupRepository.InsertAsync(input2);
        
        var rule1 = new Rule
        {
            RuleCode = "Rule1",
            SuccessEvent = "True",
            ErrorMessage = "False",
            ErrorType = default,
            RuleExpressionType = default,
            Expression = "x.Name == \"Test\""
        };
        
        var rule2 = new Rule
        {
            RuleCode = "Rule2",
            SuccessEvent = "True",
            ErrorMessage = "False",
            ErrorType = default,
            RuleExpressionType = default,
            Expression = "Convert.ToInt32(x.Age) >=20"
        };
        
        var ruleResult1 =  await _ruleRepository.InsertAsync(rule1);
        var ruleResult2 =  await _ruleRepository.InsertAsync(rule2);
        
        var rulesMember1 = new RulesMember()
        {
            Sequence = 1,
            RulesGroupId = rulesGroupResult1.Id,
            RuleId = ruleResult1.Id,
            Description = "ca9a209fe8a243c39ef67dbc66299168253ceb"
        };
        var rulesMember2 = new RulesMember()
        {
            Sequence = 2,
            RulesGroupId = rulesGroupResult1.Id,
            RuleId = ruleResult2.Id,
            Description = "ca9a209fe8a243c39ef67dbc66299168253ceb"
        };
        var rulesMember3 = new RulesMember()
        {
            Sequence = 1,
            RulesGroupId = rulesGroupResult2.Id,
            RuleId = ruleResult1.Id,
            Description = "ca9a209fe8a243c39ef67dbc66299168253ceb"
        };
        var rulesMember4 = new RulesMember()
        {
            Sequence = 2,
            RulesGroupId = rulesGroupResult2.Id,
            RuleId = ruleResult2.Id,
            Description = "ca9a209fe8a243c39ef67dbc66299168253ceb"
        };
        var rulesMemberResult1 = await _rulesMemberRepository.InsertAsync(rulesMember1);
        var rulesMemberResult2 = await _rulesMemberRepository.InsertAsync(rulesMember2);
        var rulesMemberResult3 = await _rulesMemberRepository.InsertAsync(rulesMember3);
        var rulesMemberResult4 = await _rulesMemberRepository.InsertAsync(rulesMember4);
        
        // Assert
        //TestGroup-Or判断通过返回True
        var result1 = await _rulesGroupsAppService.VerifyRulesGroupAsync(new VerifyRuleGroupDto()
        {
            GroupName = "TestRule1",
            ExtraProperties = new ExtraPropertyDictionary()
            {
                {"Name", "Test"},
                {"Age", 20}
            },
        });
        result1.IsSuccess.ShouldBeTrue();
        var result2 = await _rulesGroupsAppService.VerifyRulesGroupAsync(new VerifyRuleGroupDto()
        {
            GroupName = "TestGroup-Or",
            ExtraProperties = new ExtraPropertyDictionary()
            {
                {"Name", "Test"},
                {"Age", 20}
            },
        });
        result2.IsSuccess.ShouldBeTrue();
        
        //TestGroup-Or判断不通过返回False
        var result3 =  await _rulesGroupsAppService.VerifyRulesGroupAsync(new VerifyRuleGroupDto()
        {
            GroupName = "TestGroup-Or",
            ExtraProperties = new ExtraPropertyDictionary()
            {
                {"Name", "Test1"},
                {"Age", 19}
            },
        });
        result3.IsSuccess.ShouldBeFalse();
        
        //TestGroupAnd判断通过返回True
        var result4 = await _rulesGroupsAppService.VerifyRulesGroupAsync(new VerifyRuleGroupDto()
        {
            GroupName = "TestGroupAnd",
            ExtraProperties = new ExtraPropertyDictionary()
            {
                {"Name", "Test"},
                {"Age", 20}
            },
        });
        result4.IsSuccess.ShouldBeTrue();
        
        //TestGroupAnd判断不通过返回False
        var result5 = await _rulesGroupsAppService.VerifyRulesGroupAsync(new VerifyRuleGroupDto()
        {
            GroupName = "TestGroupAnd",
            ExtraProperties = new ExtraPropertyDictionary()
            {
                {"Name", "Test"},
                {"Age", 19}
            },
        });
        result5.IsSuccess.ShouldBeFalse();
        }
    }
}