using BotSharp.Abstraction.Conversations.Enums;

namespace BotSharp.Abstraction.States.Models;

public class StateContent
{
    public string Key { get; set; }
    public bool Versioning { get; set; }
    public bool Readonly { get; set; }
    public List<StateData> Values { get; set; } = new List<StateData>();
}


public class StateData
{
    public string Data { get; set; }

    [JsonPropertyName("message_id")]
    public string? MessageId { get; set; }

    public bool Active { get; set; }

    [JsonPropertyName("active_rounds")]
    public int ActiveRounds { get; set; }

    [JsonPropertyName("data_type")]
    public string DataType { get; set; } = StateDataType.String;

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("update_time")]
    public DateTime UpdateTime { get; set; }

    public StateData()
    {

    }

    public override string ToString()
    {
        var isActive = Active ? "Yes" : "No";
        var activeRounds = ActiveRounds <= 0 ? "infinity" : ActiveRounds.ToString();
        return $"Data: {Data}, Active: {isActive}, Active rounds: {activeRounds}, Source: {Source}";
    }
}