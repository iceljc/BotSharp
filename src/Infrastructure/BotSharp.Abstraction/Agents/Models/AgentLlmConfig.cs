namespace BotSharp.Abstraction.Agents.Models;

public class AgentLlmConfig
{
    /// <summary>
    /// Is inherited from default Agent Settings
    /// </summary>
    [JsonPropertyName("is_inherit")]
    public bool IsInherit { get; set; }

    /// <summary>
    /// Completion Provider
    /// </summary>
    [JsonPropertyName("provider")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Provider { get; set; }

    /// <summary>
    /// Model name
    /// </summary>
    [JsonPropertyName("model")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Model { get; set; }

    /// <summary>
    /// Max recursion depth
    /// </summary>
    [JsonPropertyName("max_recursion_depth")]
    public int MaxRecursionDepth { get; set; } = 3;

    /// <summary>
    /// Max output token
    /// </summary>
    [JsonPropertyName("max_output_tokens")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MaxOutputTokens { get; set; }
}
