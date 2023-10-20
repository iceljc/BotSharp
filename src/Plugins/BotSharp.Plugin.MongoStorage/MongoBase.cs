using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BotSharp.Plugin.MongoStorage;

[BsonIgnoreExtraElements(Inherited = true)]
public class MongoBase : IMongoCollection
{
    [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
    public string Id { get; set; }
}
