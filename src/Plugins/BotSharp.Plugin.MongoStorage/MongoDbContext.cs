using BotSharp.Plugin.MongoStorage.Collections;
using EntityFrameworkCore.BootKit;

namespace BotSharp.Plugin.MongoStorage;

public class MongoDbContext : Database
{
    private readonly string _collectionPrefix;

    public MongoDbContext(string prefix)
    {
        _collectionPrefix = prefix.IfNullOrEmptyAs("BotSharp");
    }

    public IMongoCollection<AgentCollection> Agents
        => Collection<AgentCollection>($"{_collectionPrefix}_Agents");

    public IMongoCollection<ConversationCollection> Conversations
        => Collection<ConversationCollection>($"{_collectionPrefix}_Conversations");

    public IMongoCollection<ConversationDialogCollection> ConversationDialogs
        => Collection<ConversationDialogCollection>($"{_collectionPrefix}_ConversationDialogs");

    public IMongoCollection<UserCollection> Users
        => Collection<UserCollection>($"{_collectionPrefix}_Users");

    public IMongoCollection<UserAgentCollection> UserAgents
        => Collection<UserAgentCollection>($"{_collectionPrefix}_UserAgents");
}
