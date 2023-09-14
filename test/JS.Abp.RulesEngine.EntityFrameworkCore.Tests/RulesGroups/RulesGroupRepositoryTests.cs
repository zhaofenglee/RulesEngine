using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using JS.Abp.RulesEngine.RulesGroups;
using JS.Abp.RulesEngine.EntityFrameworkCore;
using Xunit;

namespace JS.Abp.RulesEngine.RulesGroups
{
    public class RulesGroupRepositoryTests : RulesEngineEntityFrameworkCoreTestBase
    {
        private readonly IRulesGroupRepository _rulesGroupRepository;

        public RulesGroupRepositoryTests()
        {
            _rulesGroupRepository = GetRequiredService<IRulesGroupRepository>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _rulesGroupRepository.GetListAsync(
                    groupName: "8494fd62e3034b47886008ddd887dcd530f9149091fa420bb495ba9331e94e61869773582a3145be92156d36a26efb69c247722db4344339a2b71c4d1e49c513",
                    operatorType: default,
                    description: "4f82387eb1554255a8d6469d53b3abc67abeabf839e9437ba069b2f8c726ba6556"
                );

                // Assert
                result.Count.ShouldBe(1);
                result.FirstOrDefault().ShouldNotBe(null);
                result.First().Id.ShouldBe(Guid.Parse("15d16d9c-bb47-40e7-ad2d-60075e6c8b11"));
            });
        }

        [Fact]
        public async Task GetCountAsync()
        {
            // Arrange
            await WithUnitOfWorkAsync(async () =>
            {
                // Act
                var result = await _rulesGroupRepository.GetCountAsync(
                    groupName: "51a5bd9c44304df182a6aecfe4942823695e14bc93de4ca5b9f0028ce8d7cd28c7e51ce679bc42dcbf44e956e9a2e1a6c55fa7d9afda4279abf7f9a3c1cf9a3e",
                    operatorType: default,
                    description: "c1d7737e8cf3439080"
                );

                // Assert
                result.ShouldBe(1);
            });
        }
    }
}