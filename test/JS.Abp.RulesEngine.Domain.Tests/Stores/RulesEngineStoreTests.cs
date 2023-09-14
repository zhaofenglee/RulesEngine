using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JS.Abp.RulesEngine.OperatorTypes;
using JS.Abp.RulesEngine.Rules;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.RulesMembers;
using Shouldly;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace JS.Abp.RulesEngine.Stores;

public class RulesEngineStoreTests : RulesEngineDomainTestBase
{
    private readonly IRepository<Rule, Guid> _ruleRepository;
    private readonly IRepository<RulesGroup, Guid> _rulesGroupRepository;
    private readonly IRepository<RulesMember, Guid> _rulesMemberRepository;
    private readonly IRulesEngineStore _rulesEngineStore;
    public RulesEngineStoreTests()
    {   
        _ruleRepository = GetRequiredService<IRepository<Rule, Guid>>();
        _rulesGroupRepository = GetRequiredService<IRepository<RulesGroup, Guid>>();
        _rulesMemberRepository = GetRequiredService<IRepository<RulesMember, Guid>>();
        _rulesEngineStore = GetRequiredService<IRulesEngineStore>();
    }
    
    [Fact]
    public async Task ExecuteRulesByNameAsync()
    {
        // Arrange
        //创建一个Test1规则
        var input = new Rule
        {
            RuleCode = "Test1",
            SuccessEvent = "True",
            ErrorMessage = "False",
            ErrorType = default,
            RuleExpressionType = default,
            Expression = "x.Name == \"Test\" && x.Age >= 20"
        };
        // Act
        var insertResult =  await _ruleRepository.InsertAsync(input);
        
        // Assert
        //TestRule1不存在，会默认返回True
        var result1 = await _rulesEngineStore.ExecuteRulesAsync("TestRule1", new TestDto(){Name = "Test",Age = 20});
        result1.IsSuccess.ShouldBeTrue();
        //Test1判断通过返回True
        var result2 = await _rulesEngineStore.ExecuteRulesAsync("Test1", new TestDto(){Name = "Test",Age = 20});
        result2.IsSuccess.ShouldBeTrue();
        //Test1判断不通过False
        var result3 = await _rulesEngineStore.ExecuteRulesAsync("Test1", new TestDto(){Name = "TestRule",Age = 20});
        result3.IsSuccess.ShouldBeFalse();

    }
 
    [Fact]
    public async Task ExecuteRulesByIdAsync()
    {
        // Arrange
        //创建一个Test1规则
        var input = new Rule
        {
            RuleCode = "Test1",
            SuccessEvent = "True",
            ErrorMessage = "False",
            ErrorType = default,
            RuleExpressionType = default,
            Expression = "x.Name == \"Test\" && x.Age >= 20"
        };
        // Act
        var insertResult =  await _ruleRepository.InsertAsync(input);
        
        // Assert
        //Id不存在，会默认返回True
        var result1 = await _rulesEngineStore.ExecuteRulesAsync(Guid.NewGuid(), new TestDto(){Name = "Test",Age = 20});
        result1.IsSuccess.ShouldBeTrue();
        //Test1判断通过返回True
        var result2 = await _rulesEngineStore.ExecuteRulesAsync(input.Id, new TestDto(){Name = "Test",Age = 20});
        result2.IsSuccess.ShouldBeTrue();
        //Test1判断不通过False
        var result3 = await _rulesEngineStore.ExecuteRulesAsync(input.Id, new TestDto(){Name = "TestRule",Age = 20});
        result3.IsSuccess.ShouldBeFalse();
    }


    [Fact]
    public async Task ExecuteRulesGroupAsync()
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
            Expression = "x.Age >= 20"
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
        var result1 = await _rulesEngineStore.ExecuteRulesGroupAsync("TestGroup-Or", new TestDto(){Name = "Test",Age = 19});
        result1.IsSuccess.ShouldBeTrue();
        var result2 = await _rulesEngineStore.ExecuteRulesGroupAsync("TestGroup-Or", new TestDto(){Name = "Test1",Age = 20});
        result2.IsSuccess.ShouldBeTrue();
        
        //TestGroup-Or判断不通过返回False
        var result3 = await _rulesEngineStore.ExecuteRulesGroupAsync("TestGroup-Or", new TestDto(){Name = "Test1",Age = 19});
        result3.IsSuccess.ShouldBeFalse();
        
        //TestGroupAnd判断通过返回True
        var result4 = await _rulesEngineStore.ExecuteRulesGroupAsync("TestGroupAnd", new TestDto(){Name = "Test",Age = 20});
        result4.IsSuccess.ShouldBeTrue();
        
        //TestGroupAnd判断不通过返回False
        var result5 = await _rulesEngineStore.ExecuteRulesGroupAsync("TestGroupAnd", new TestDto(){Name = "Test1",Age = 19});
        result5.IsSuccess.ShouldBeFalse();
    }
}