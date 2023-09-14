using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace JS.Abp.RulesEngine.RulesGroups
{
    public class RulesGroupsAppServiceTests : RulesEngineApplicationTestBase
    {
        private readonly IRulesGroupsAppService _rulesGroupsAppService;
        private readonly IRepository<RulesGroup, Guid> _rulesGroupRepository;

        public RulesGroupsAppServiceTests()
        {
            _rulesGroupsAppService = GetRequiredService<IRulesGroupsAppService>();
            _rulesGroupRepository = GetRequiredService<IRepository<RulesGroup, Guid>>();
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
    }
}