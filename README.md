# 规则引擎

---

## 实现原理
根据特定的规则模型，根据判断表达式判断传入数据集是否符合
* 如果传入规则不存在，默认会判断通过(返回True)
* 表达式默认是以x.开头，x.后面的是传入的数据集的属性名

## 准备工作

### 1.安装 NuGet packages.
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

### 2.添加“依赖”属性以配置模块
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

### *若使用MongoDb，以下步骤可以忽略
### 3. 在你的Dbcontext添加 ` builder.ConfigureRulesEngine();`

### 4. 添加 EF Core 迁移并更新数据库
在 YourProject.EntityFrameworkCore 项目的目录中打开命令行终端，然后键入以下命令：

````bash
> dotnet ef migrations add Added_RulesEngine
````
````bash
> dotnet ef database update
````

## 如何使用
### 1.维护规则
MVC和Blazor已经实现了维护规则的页面，如果你的项目中没有使用这两个框架，你可以自己实现维护规则的页面

### 2.在你的项目中使用
#### 2.1使用Store(参考测试项目RulesEngineStoreTests)
````csharp
        protected IRulesEngineStore rulesEngine => LazyServiceProvider.LazyGetRequiredService<IRulesEngineStore>();
        //如下是维护的规则，正式代码下可不用
        var input = new Rule
        {
            RuleName = "Test1",
            SuccessEvent = "True",
            ErrorMessage = "False",
            ErrorType = default,
            RuleExpressionType = default,
            Expression = "x.Name == \"Test\" && x.Age >= 20"
        };
        //TestRule1不存在，会默认返回True
        var result1 = await _rulesEngineStore.ExecuteRulesAsync("TestRule1", new TestDto(){Name = "Test",Age = 20});
        //Test1判断通过返回True
        var result2 = await _rulesEngineStore.ExecuteRulesAsync("Test1", new TestDto(){Name = "Test",Age = 20});
        result2.IsSuccess.ShouldBeTrue();
        //Test1判断不通过False
        var result3 = await _rulesEngineStore.ExecuteRulesAsync("Test1", new TestDto(){Name = "TestRule",Age = 20});
        result3.IsSuccess.ShouldBeFalse();
````
#### 2.2使用API(参考测试项目RuleApplicationTests,RulesGroupApplicationTests)
```csharp
//使用规则引擎组
private readonly IRulesGroupsAppService _rulesGroupsAppService;

 var result1 = await _rulesAppService.VerifyRuleAsync(new VerifyRuleDto()
            {
                RuleCode = "TestRule1",
                ExtraProperties = new ExtraPropertyDictionary()//支持以Dictionary<string,object>传入
                {
                    {"Name", "Test"},
                    {"Age", 20}
                },
            });
//使用规则引擎
private readonly IRulesAppService _rulesAppService;
var result1 = await _rulesGroupsAppService.VerifyRulesGroupAsync(new VerifyRuleGroupDto()
        {
            GroupName = "TestRule1",
            ExtraProperties = new ExtraPropertyDictionary()//支持以Dictionary<string,object>传入
            {
                {"Name", "Test"},
                {"Age", 20}
            },
        });
```

## Sample
以电商为例，假设有如下规则
* 1.价格大于1000元满减500元
* 2.价格大于500元满减300元
* 3.价格大于200元满减100元
* 4.价格大于100元满减20元
### 1.维护规则
![2023092301](/docs/images/2023092301.png)
### 2.维护规则组
#### 2.1建一个规则组”TestPrice“
#### 2.2把规则1-4添加到规则组”TestPrice“
![2023092302](/docs/images/2023092302.png)
### 3.使用规则引擎
* 1.可以现在前端判断一次，把结果返回前端
* 2.后端再判断一次，判断前端传入和后端计算结果是否一致
参考代码:JS.Abp.RulesEngine.Blazor.Server.Host/Pages/Books.razor
````csharp
//判断是否有折扣
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


````
![2023092303](/docs/images/2023092303.png)
