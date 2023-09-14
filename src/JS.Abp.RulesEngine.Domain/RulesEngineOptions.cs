namespace JS.Abp.RulesEngine;

public class RulesEngineOptions
{
    public int CacheExpirationTime { get;set; }
    
    public RulesEngineOptions()
    {
        CacheExpirationTime = 10;
    }
}