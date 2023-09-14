using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public class RulesMembersAppServiceTests : RulesEngineApplicationTestBase
    {
        private readonly IRulesMembersAppService _rulesMembersAppService;
        private readonly IRepository<RulesMember, Guid> _rulesMemberRepository;

        public RulesMembersAppServiceTests()
        {
            _rulesMembersAppService = GetRequiredService<IRulesMembersAppService>();
            _rulesMemberRepository = GetRequiredService<IRepository<RulesMember, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _rulesMembersAppService.GetListAsync(new GetRulesMembersInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.RulesMember.Id == Guid.Parse("f8402a52-f7d4-4f69-87ae-bb5df23f2348")).ShouldBe(true);
            result.Items.Any(x => x.RulesMember.Id == Guid.Parse("b42abf30-a721-493c-bcc5-3bf8c9316716")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _rulesMembersAppService.GetAsync(Guid.Parse("f8402a52-f7d4-4f69-87ae-bb5df23f2348"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("f8402a52-f7d4-4f69-87ae-bb5df23f2348"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new RulesMemberCreateDto
            {
                Sequence = 1047254554,
                Description = "f0bdccca8a1243bd877f76f2e7cb89880d63509d6a204e3e9b8ef730c4aebad7112"
            };

            // Act
            var serviceResult = await _rulesMembersAppService.CreateAsync(input);

            // Assert
            var result = await _rulesMemberRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Sequence.ShouldBe(1047254554);
            result.Description.ShouldBe("f0bdccca8a1243bd877f76f2e7cb89880d63509d6a204e3e9b8ef730c4aebad7112");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new RulesMemberUpdateDto()
            {
                Sequence = 330357827,
                Description = "d959a0f2746d4289b291fdad3c694144ed443dcce96345e1bef455e4b46055c30050dc3dbc71410bad8e49"
            };

            // Act
            var serviceResult = await _rulesMembersAppService.UpdateAsync(Guid.Parse("f8402a52-f7d4-4f69-87ae-bb5df23f2348"), input);

            // Assert
            var result = await _rulesMemberRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.Sequence.ShouldBe(330357827);
            result.Description.ShouldBe("d959a0f2746d4289b291fdad3c694144ed443dcce96345e1bef455e4b46055c30050dc3dbc71410bad8e49");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _rulesMembersAppService.DeleteAsync(Guid.Parse("f8402a52-f7d4-4f69-87ae-bb5df23f2348"));

            // Assert
            var result = await _rulesMemberRepository.FindAsync(c => c.Id == Guid.Parse("f8402a52-f7d4-4f69-87ae-bb5df23f2348"));

            result.ShouldBeNull();
        }
    }
}