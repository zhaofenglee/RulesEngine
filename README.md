# 规则引擎

---

## 实现原理
根据特定的规则模型，根据判断表达式判断传入数据集是否符合
* 如果传入规则不存在，默认会判断通过(返回True)
* 表达式默认是以x.开头，x.后面的是传入的数据集的属性名

## 如何使用
### 1.维护规则
MVC和Blazor已经实现了维护规则的页面，如果你的项目中没有使用这两个框架，你可以自己实现维护规则的页面

### 2.在你的项目中使用

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
