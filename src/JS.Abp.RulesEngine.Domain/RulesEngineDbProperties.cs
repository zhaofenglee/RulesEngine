namespace JS.Abp.RulesEngine;

public static class RulesEngineDbProperties
{
    public static string DbTablePrefix { get; set; } = "RulesEngine";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "RulesEngine";
}
