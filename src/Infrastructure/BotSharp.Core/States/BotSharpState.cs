namespace BotSharp.Core.States;

public class BotSharpState : IBotSharpState, IDisposable
{
    private readonly IServiceProvider _services;
    private readonly IBotSharpRepository _db;
    private readonly ILogger<BotSharpState> _logger;
    private string _sessionId;

    public BotSharpState(IServiceProvider services,
        IBotSharpRepository db,
        ILogger<BotSharpState> logger)
    {
        _services = services;
        _db = db;
        _logger = logger;
    }

    public string GetSessionId()
    {
        return _sessionId;
    }

    public string GetState(string name, string defaultValue = "")
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, string> GetStates()
    {
        throw new NotImplementedException();
    }

    public bool ContainsState(string name)
    {
        throw new NotImplementedException();
    }

    public IConversationStateService SetState<T>(string name, T value)
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, string> Load(string sessionId, bool isReadOnly = false)
    {
        throw new NotImplementedException();
    }

    public bool RemoveState(string name)
    {
        throw new NotImplementedException();
    }

    public void CleanStates()
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }

    public void SaveStateByArgs(JsonDocument args)
    {
        if (args == null) return;

        if (args.RootElement is JsonElement root)
        {
            foreach (JsonProperty property in root.EnumerateObject())
            {
                if (!string.IsNullOrEmpty(property.Value.ToString()))
                {
                    SetState(property.Name, property.Value);
                }
            }
        }
    }

    public void Dispose()
    {
        Save();
    }
}
