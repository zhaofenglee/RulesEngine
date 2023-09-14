using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JS.Abp.RulesEngine;

public interface IRulesEngineStore
{
    /// <summary>
    /// 执行规则引擎
    /// </summary>
    /// <param name="ruleCode"></param>
    /// <param name="input"></param>
    /// <typeparam name="TDto"></typeparam>
    /// <returns></returns>
    Task<RuleResult> ExecuteRulesAsync<TDto>(string ruleCode, TDto input);
    Task<RuleResult> ExecuteRulesAsync<TDto>(Guid id, TDto input);
    /// <summary>
    /// 执行规则引擎
    /// </summary>
    /// <param name="ruleCode"></param>
    /// <param name="input"></param>
    /// <typeparam name="TDto"></typeparam>
    /// <returns></returns>
    Task<List<RuleResult>> ExecuteAllRulesAsync<TDto>(string ruleCode, IEnumerable<TDto> input);
    Task<List<RuleResult>> ExecuteAllRulesAsync<TDto>(Guid id, IEnumerable<TDto> input);
    /// <summary>
    /// 执行规则引擎组
    /// </summary>
    /// <param name="ruleGroupName"></param>
    /// <param name="input"></param>
    /// <typeparam name="TDto"></typeparam>
    /// <returns></returns>
    Task<RuleResult> ExecuteRulesGroupAsync<TDto>(string ruleGroupName, TDto input);
    Task<List<RuleResult>> ExecuteAllRulesGroupAsync<TDto>(string ruleGroupName, IEnumerable<TDto> input);
}