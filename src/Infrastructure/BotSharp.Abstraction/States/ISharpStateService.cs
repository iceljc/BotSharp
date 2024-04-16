using BotSharp.Abstraction.Conversations;
using System.Text.Json;

namespace BotSharp.Abstraction.States;

public interface ISharpStateService
{

    string GetSessionId();
    Dictionary<string, string> Load(string sessionId, bool isReadOnly = false);
    string GetState(string name, string defaultValue = "");
    bool ContainsState(string name);
    Dictionary<string, string> GetStates();
    IConversationStateService SetState<T>(string name, T value);
    void SaveStateByArgs(JsonDocument args);
    bool RemoveState(string name);
    void CleanStates();
    void Save();
}
