using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using JS.Abp.RulesEngine.RulesMembers;
using JS.Abp.RulesEngine.EntityFrameworkCore;
using Xunit;

namespace JS.Abp.RulesEngine.RulesMembers
{
    public class RulesMemberRepositoryTests : RulesEngineEntityFrameworkCoreTestBase
    {
        private readonly IRulesMemberRepository _rulesMemberRepository;

        public RulesMemberRepositoryTests()
        {
            _rulesMemberRepository = GetRequiredService<IRulesMemberRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _rulesMemberRepository.GetListAsync(
                    description: "15204faafddb41b3b9fe23a4ab383b0623952a07039c47349941c05178d23295d2eacf3052294bc18476e9fec5"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("f8402a52-f7d4-4f69-87ae-bb5df23f2348"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _rulesMemberRepository.GetCountAsync(
                    description: "449631a835ba4d9e8fb4d87771f4774c5025daaf4251499cbb61b40b91edca21c1853f3dc"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}