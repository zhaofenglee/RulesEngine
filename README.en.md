# Rules Engine

---

<div align="center">
<p><strong><a href="README.en.md">English</a> | <a href="README.md">简体中文</a> </strong></p>
</div>

---

## Implementation Principle
Based on a specific rule model, it judges whether the incoming data set conforms according to the judgment expression.
* If the incoming rule does not exist, it will be judged as passed by default (return True).
* The expression defaults to start with x., and the following x. is the attribute name of the incoming data set.

## Preparation

### 1. Install NuGet packages.
* JS.Abp.RulesEngine.Application
* JS.Abp.RulesEngine.Application.Contracts
* JS.Abp.RulesEngine.Domain
* JS.Abp.RulesEngine.Domain.Shared
* JS.Abp.RulesEngine.EntityFrameworkCore
* JS.Abp.RulesEngine.HttpApi
* JS.Abp.RulesEngine.HttpApi.Client

*(Optional)  JS.Abp.RulesEngine.Blazor
*(Optional)  JS.Abp.RulesEngine.Blazor.Server
*(Optional)  JS.Abp.RulesEngine.Blazor.WebAssembly
*(Optional)  JS.Abp.RulesEngine.Web

### 2. Add "DependsOn" attribute to configure the module
* [DependsOn(typeof(RulesEngineApplicationModule))]
* [DependsOn(typeof(RulesEngineApplicationContractsModule))]
* [DependsOn(typeof(RulesEngineDomainModule))]
* [DependsOn(typeof(RulesEngineDomainSharedModule))]
* [DependsOn(typeof(RulesEngineEntityFrameworkCoreModule))] OR [DependsOn(typeof(RulesEngineMongoDbModule))]
* [DependsOn(typeof(RulesEngineHttpApiModule))]
* [DependsOn(typeof(RulesEngineHttpApiClientModule))]

*(Optional)  [DependsOn(typeof(RulesEngineBlazorModule))]
*(Optional)  [DependsOn(typeof(RulesEngineBlazorServerModule))]
*(Optional)  [DependsOn(typeof(RulesEngineBlazorWebAssemblyModule))]
*(Optional)  [DependsOn(typeof(RulesEngineWebModule))]

### If using MongoDb, the following steps can be ignored
### 3. Add `builder.ConfigureRulesEngine();` to your DbContext

### 4. Add EF Core migrations and update the database
Open the command line terminal in the directory of YourProject.EntityFrameworkCore project, then type the following commands:

```bash
> dotnet ef migrations add Added_RulesEngine
```
```bash
> dotnet ef database update
```

## How to use
### 1. Maintain rules
MVC and Blazor have already implemented the pages for maintaining rules. If these two frameworks are not used in your project, you can implement the pages for maintaining rules yourself.

### 2. Use in your project
#### 2.1 Use Store (refer to the test project RulesEngineStoreTests)
```csharp
        protected IRulesEngineStore rulesEngine => LazyServiceProvider.LazyGetRequiredService<IRulesEngineStore>();
        // The following are the maintained rules, which can be ignored in the formal code
        var input = new Rule
        {
            RuleName = "Test1",
            SuccessEvent = "True",
            ErrorMessage = "False",
            ErrorType = default,
            RuleExpressionType = default,
            Expression = "x.Name == \"Test\" && x.Age >= 20"
        };
        // If TestRule1 does not exist, it will return True by default
        var result1 = await _rulesEngineStore.ExecuteRulesAsync("TestRule1", new TestDto(){Name = "Test",Age = 20});
        // Test1 passes the judgment and returns True
        var result2 = await _rulesEngineStore.ExecuteRulesAsync("Test1", new TestDto(){Name = "Test",Age = 20});
        result2.IsSuccess.ShouldBeTrue();
        // Test1 fails the judgment and returns False
        var result3 = await _rulesEngineStore.ExecuteRulesAsync("Test1", new TestDto(){Name = "TestRule",Age = 20});
        result3.IsSuccess.ShouldBeFalse();
```
#### 2.2 Use API (refer to the test projects RuleApplicationTests, RulesGroupApplicationTests)
```csharp
// Use rules engine group
private readonly IRulesGroupsAppService _rulesGroupsAppService;

 var result1 = await _rulesAppService.VerifyRuleAsync(new VerifyRuleDto()
            {
                RuleCode = "TestRule1",
                ExtraProperties = new ExtraPropertyDictionary()// Supports passing in as Dictionary<string,object>
                {
                    {"Name", "Test"},
                    {"Age", 20}
                },
            });
// Use rules engine
private readonly IRulesAppService _rulesAppService;
var result1 = await _rulesGroupsAppService.VerifyRulesGroupAsync(new VerifyRuleGroupDto()
        {
            GroupName = "TestRule1",
            ExtraProperties = new ExtraPropertyDictionary()// Supports passing in as Dictionary<string,object>
            {
                {"Name", "Test"},
                {"Age", 20}
            },
        });
```

## Sample
Take e-commerce as an example, suppose there are the following rules
* 1. Price over 1000 yuan, get 500 yuan off
* 2. Price over 500 yuan, get 300 yuan off
* 3. Price over 200 yuan, get 100 yuan off
* 4. Price over 100 yuan, get 20 yuan off
### 1. Maintain rules
![2023092301](/docs/images/2023092301.png)
### 2. Maintain rule groups
#### 2.1 Create a rule group "TestPrice"
#### 2.2 Add rules 1-4 to the rule group "TestPrice"
![2023092302](/docs/images/2023092302.png)
### 3. Use the rules engine
* 1. You can judge once on the front end and return the result to the front end
* 2. The backend judges again to check whether the result passed in by the front end is consistent with the result calculated by the backend
     Refer to the code: JS.Abp.RulesEngine.Blazor.Server.Host/Pages/Books.razor
```csharp
// Judge whether there is a discount
 var result = await RulesGroupsAppService.VerifyRulesGroupAsync(new VerifyRuleGroupDto()
        {
            GroupName = "TestPrice",
            ExtraProperties = new ExtraPropertyDictionary()
            {
                {"Price", BookList.Sum(c=>c.Price)}
            },
        });
        if (result.IsSuccess)
        {
            if (!result.SuccessEvent.IsNullOrWhiteSpace())
            {
                Discount = result.SuccessEvent;
            }
        }
```
![2023092303](/docs/images/2023092303.png)