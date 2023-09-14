using JS.Abp.RulesEngine.ErrorTypes;

namespace JS.Abp.RulesEngine;

public  class RuleResult
{
    public bool IsSuccess { get; set; }
    public string? SuccessEvent { get; set; }
    public string? ErrorMessage { get; set; }
    public ErrorType? ErrorType { get; set; }
    
    
    public RuleResult()
    {
        
    }
    
    public RuleResult(bool isSuccess, string? successEvent, string? errorMessage, ErrorType? errorType)
    {
        IsSuccess = isSuccess;
        SuccessEvent = successEvent;
        ErrorMessage = errorMessage;
        ErrorType = errorType;
    }
}