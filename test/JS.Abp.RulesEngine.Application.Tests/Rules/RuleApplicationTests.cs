using System;
using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using JS.Abp.RulesEngine.Stores;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace JS.Abp.RulesEngine.Rules
{
    public class RulesAppServiceTests : RulesEngineApplicationTestBase
    {
        private readonly IRulesAppService _rulesAppService;
        private readonly IRepository<Rule, Guid> _ruleRepository;

        public RulesAppServiceTests()
        {
            _rulesAppService = GetRequiredService<IRulesAppService>();
            _ruleRepository = GetRequiredService<IRepository<Rule, Guid>>();
        }

        [Fact]
        public async Task GetListAsync()
        {
            // Act
            var result = await _rulesAppService.GetListAsync(new GetRulesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("60342e5a-dc76-47a3-9cb1-8a1eb19341cb")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("4e3b6ec6-1ed3-4e5c-a057-d863159f7cb2")).ShouldBe(true);
        }

        [Fact]
        public async Task GetAsync()
        {
            // Act
            var result = await _rulesAppService.GetAsync(Guid.Parse("60342e5a-dc76-47a3-9cb1-8a1eb19341cb"));

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Guid.Parse("60342e5a-dc76-47a3-9cb1-8a1eb19341cb"));
        }

        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var input = new RuleCreateDto
            {
                RuleCode = "7220ea5ef70f4b81ae624289958d4a35a02184fbad36475a9590ba1cb45662de8255896a3cc7423992dbe80041fb0f61f183a0dd7ce44e44a514fece99389eac",
                SuccessEvent = "80d9ceaee2094842993f3e6378fbf79171ab35c838e44178993e72aaaf4000d4a4a316c82daa400c99b0f59387598b5058618688a5be4d51bd2fa81514cbf887",
                ErrorMessage = "4f1153814efe4648b768816703075c82a1d7eb4057aa435b8503a629756f3a2c304ca0827b2e4bada354dbffad5807a87e7e4d19dbf5457eae576587435e66e888f69fd7a6c246c293dbbfe98d0bc701a6f3766c842744ddbb4b66212bac59b6675dc4fe806948afa12191cf7b467ff3df27ce7cb6e94c599a9dd01ac2638011c909c8e5c98640eaa63d8764aa93bd1ada98806c04274ea0843e30f36a54a48375f7504145084d0c96a6554c7605e4949e0c301ee7f24315b501426622824187b97984fed85e4bbdb6c0e8f8d1cb93df75cc9c66796148688367e1e72d624072cd2f7e392f8e4867a70591b4abdde4ce2a6cfc288b494a62ab34a2f835f9b4f9",
                ErrorType = default,
                RuleExpressionType = default,
                Expression = "a69fa17ff64c4ab09d6dd2c5d29d25813c3b4",
                Description = "ed6a570c5bdf433d90dbf2fd0899cca6b60aa902dc9241d4ab4f"
            };

            // Act
            var serviceResult = await _rulesAppService.CreateAsync(input);

            // Assert
            var result = await _ruleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.RuleCode.ShouldBe("7220ea5ef70f4b81ae624289958d4a35a02184fbad36475a9590ba1cb45662de8255896a3cc7423992dbe80041fb0f61f183a0dd7ce44e44a514fece99389eac");
            result.SuccessEvent.ShouldBe("80d9ceaee2094842993f3e6378fbf79171ab35c838e44178993e72aaaf4000d4a4a316c82daa400c99b0f59387598b5058618688a5be4d51bd2fa81514cbf887");
            result.ErrorMessage.ShouldBe("4f1153814efe4648b768816703075c82a1d7eb4057aa435b8503a629756f3a2c304ca0827b2e4bada354dbffad5807a87e7e4d19dbf5457eae576587435e66e888f69fd7a6c246c293dbbfe98d0bc701a6f3766c842744ddbb4b66212bac59b6675dc4fe806948afa12191cf7b467ff3df27ce7cb6e94c599a9dd01ac2638011c909c8e5c98640eaa63d8764aa93bd1ada98806c04274ea0843e30f36a54a48375f7504145084d0c96a6554c7605e4949e0c301ee7f24315b501426622824187b97984fed85e4bbdb6c0e8f8d1cb93df75cc9c66796148688367e1e72d624072cd2f7e392f8e4867a70591b4abdde4ce2a6cfc288b494a62ab34a2f835f9b4f9");
            result.ErrorType.ShouldBe(default);
            result.RuleExpressionType.ShouldBe(default);
            result.Expression.ShouldBe("a69fa17ff64c4ab09d6dd2c5d29d25813c3b4");
            result.Description.ShouldBe("ed6a570c5bdf433d90dbf2fd0899cca6b60aa902dc9241d4ab4f");
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var input = new RuleUpdateDto()
            {
                RuleCode = "c130e94ff0954b1ba974f1adab1956f602e063734a5e4d228953547b229a47a0abf0cadb48384bc699d848ac0eabecbd86553c8a8bf44241ba4992ba8ca79267",
                SuccessEvent = "2143e7b8b41d434486233049c20179312a2e9bfb255348088fa7869a59cbca676539cd39c8bd4654a6f0c0704ea2027126a6f437d8c3479d9f043aea5a2e2434",
                ErrorMessage = "0ba45b77582f40388669c7528e372468a451887a41174046ab139b2ee383779631e80ca59941470487ca90812998c808c683079f15b040988c03e94404a9b34df9d2a13e64c6400d8bc9a47843e127063f323e6eabf34f9b8dce1c7ea08c33b70b87970232ac4eba8ebf96460b4d5d493f712636766b4f93becd78e5adf5b5b47959fcbf7e1b448c84f464f3a32342db148f61de24984c23bee2707a7e1c3281cefb5fd3c996404187493e0c6acb90853481874eceb54eafb2ba5b2009d00622744370dbc2e04433965279e4d9453978beacbf522247461ab9631b8b9bb376059583bd24c0da4950abf0ac53771059a6c28ed6ffdda342e799c66a5ebbbc48d3",
                ErrorType = default,
                RuleExpressionType = default,
                Expression = "2be1844ba4af437990621",
                Description = "23c193a0fc744570a181ddc4a3b9a226d0d6eba02bfe4c0da79058d1579d11af36401814d9e4478e83e03a"
            };

            // Act
            var serviceResult = await _rulesAppService.UpdateAsync(Guid.Parse("60342e5a-dc76-47a3-9cb1-8a1eb19341cb"), input);

            // Assert
            var result = await _ruleRepository.FindAsync(c => c.Id == serviceResult.Id);

            result.ShouldNotBe(null);
            result.RuleCode.ShouldBe("c130e94ff0954b1ba974f1adab1956f602e063734a5e4d228953547b229a47a0abf0cadb48384bc699d848ac0eabecbd86553c8a8bf44241ba4992ba8ca79267");
            result.SuccessEvent.ShouldBe("2143e7b8b41d434486233049c20179312a2e9bfb255348088fa7869a59cbca676539cd39c8bd4654a6f0c0704ea2027126a6f437d8c3479d9f043aea5a2e2434");
            result.ErrorMessage.ShouldBe("0ba45b77582f40388669c7528e372468a451887a41174046ab139b2ee383779631e80ca59941470487ca90812998c808c683079f15b040988c03e94404a9b34df9d2a13e64c6400d8bc9a47843e127063f323e6eabf34f9b8dce1c7ea08c33b70b87970232ac4eba8ebf96460b4d5d493f712636766b4f93becd78e5adf5b5b47959fcbf7e1b448c84f464f3a32342db148f61de24984c23bee2707a7e1c3281cefb5fd3c996404187493e0c6acb90853481874eceb54eafb2ba5b2009d00622744370dbc2e04433965279e4d9453978beacbf522247461ab9631b8b9bb376059583bd24c0da4950abf0ac53771059a6c28ed6ffdda342e799c66a5ebbbc48d3");
            result.ErrorType.ShouldBe(default);
            result.RuleExpressionType.ShouldBe(default);
            result.Expression.ShouldBe("2be1844ba4af437990621");
            result.Description.ShouldBe("23c193a0fc744570a181ddc4a3b9a226d0d6eba02bfe4c0da79058d1579d11af36401814d9e4478e83e03a");
        }

        [Fact]
        public async Task DeleteAsync()
        {
            // Act
            await _rulesAppService.DeleteAsync(Guid.Parse("60342e5a-dc76-47a3-9cb1-8a1eb19341cb"));

            // Assert
            var result = await _ruleRepository.FindAsync(c => c.Id == Guid.Parse("60342e5a-dc76-47a3-9cb1-8a1eb19341cb"));

            result.ShouldBeNull();
        }
        
        [Fact]
        public async Task VerifyRuleAsync()
        {
            // Act
            var result = await _rulesAppService.GetListAsync(new GetRulesInput());

            // Assert
            result.TotalCount.ShouldBe(2);
            result.Items.Count.ShouldBe(2);
            result.Items.Any(x => x.Id == Guid.Parse("60342e5a-dc76-47a3-9cb1-8a1eb19341cb")).ShouldBe(true);
            result.Items.Any(x => x.Id == Guid.Parse("4e3b6ec6-1ed3-4e5c-a057-d863159f7cb2")).ShouldBe(true);
            
            
            // Arrange
            //创建一个Test1规则
            var input = new Rule
            {
                RuleCode = "Test1",
                SuccessEvent = "True",
                ErrorMessage = "False",
                ErrorType = default,
                RuleExpressionType = default,
                Expression = "x.Name == \"Test\" && Convert.ToInt32(x.Age) >=20" //需要注意是如果是Object类型判断需要转换类型再判断
            };
            // Act
            var insertResult =  await _ruleRepository.InsertAsync(input);
        
            // Assert
            //TestRule1不存在，会默认返回True
            ExtraPropertyDictionary extraPropertyDictionary = new ExtraPropertyDictionary();
            var result1 = await _rulesAppService.VerifyRuleAsync(new VerifyRuleDto()
            {
                RuleCode = "TestRule1",
                ExtraProperties = new ExtraPropertyDictionary()
                {
                    {"Name", "Test"},
                    {"Age", 20}
                },
            });
            result1.IsSuccess.ShouldBeTrue();
            //Test1判断通过返回True
            var result2 = await _rulesAppService.VerifyRuleAsync(new VerifyRuleDto()
            {
                RuleCode = "Test1",
                ExtraProperties = new ExtraPropertyDictionary()
                {
                    {"Name", "Test"},
                    {"Age", 20}
                },
            });
            result2.IsSuccess.ShouldBeTrue();
            //Test1判断不通过False
            var result3 = await _rulesAppService.VerifyRuleAsync(new VerifyRuleDto()
            {
                RuleCode = "Test1",
                ExtraProperties = new ExtraPropertyDictionary()
                {
                    {"Name", "Test"},
                    {"Age", 19}
                },
            });
            result3.IsSuccess.ShouldBeFalse();
        }
    }
}