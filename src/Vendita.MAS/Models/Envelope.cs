using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Vendita.MAS.Models
{
    public class Envelope
    {
        public int Page { get; internal set; } = 1;
        public int PageCount { get; internal set; } = 1;
    }

    [JsonConverter(typeof(EnvelopeConverter))]
    public class Envelope<T>: Envelope
    {
        public Envelope()
        {
        }

        public T[] Contents { get; internal set; } = new T[0];
    }

    public class EnvelopeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Envelope).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject) throw new JsonSerializationException();
            var container = JObject.Load(reader);
            var data = (JObject)container["data"];
            var page = data["page"]?.ToObject<int>() ?? 1;
            var pageCount = data["page_count"]?.ToObject<int>() ?? 1;
            var envelope = (Envelope)Activator.CreateInstance(objectType);
            envelope.Page = page;
            envelope.PageCount = pageCount;
            var recordField = data["record_field"]?.ToObject<string>();
            if (recordField != null && objectType.GenericTypeArguments.Length == 1)
            {
                var array = data[recordField];
                var prop = objectType.GetRuntimeProperty(nameof(Envelope<object>.Contents));
                var type = objectType.GenericTypeArguments[0];
                prop.SetValue(envelope, serializer.Deserialize(array.CreateReader(), type.MakeArrayType()));
            }
            return envelope;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
