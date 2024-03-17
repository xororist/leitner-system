using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LeitnerSystem.Infrastructure.Utils;

public class ValueObjectSerializer<TValueObject> : SerializerBase<TValueObject> where TValueObject : class
{
    public override TValueObject Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var serializer = BsonSerializer.LookupSerializer(typeof(string));
        var text = serializer.Deserialize(context, args).ToString();
        var instance = Activator.CreateInstance(typeof(TValueObject), text);
        if (instance == null) 
        {
            throw new InvalidOperationException();
        }
        return (TValueObject)instance;
    }


    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, TValueObject value)
    {
        var serializer = BsonSerializer.LookupSerializer(typeof(string));
        var textProperty = value.GetType().GetProperty("Text")?.GetValue(value, null)?.ToString();
        serializer.Serialize(context, args, textProperty);
    }
}