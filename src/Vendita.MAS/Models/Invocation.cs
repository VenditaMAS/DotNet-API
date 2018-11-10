using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Vendita.MAS.Models
{
    public class Invocation
    {
        public enum InvocationStatus
        {
            Unknown,
            Aborted,
            Executing,
            Failed,
            Scheduled,
            Succeeded
        }

        [JsonConverter(typeof(InvocationOutputConverter))]
        public class Output
        {
            public Output(InvocationStatus status)
            {
                Status = status;
            }

            public InvocationStatus Status { get; private set; }
        }

        public class TextOutput: Output
        {
            public TextOutput(InvocationStatus status, string text)
                : base(status)
            {
                Text = text;
            }

            public string Text { get; private set; }
        }

        public class ProgressOutput: Output
        {
            public ProgressOutput(InvocationStatus status, float progress)
                : base(status)
            {
                Progress = progress;
            }

            public float Progress { get; private set; }
        }

        [JsonProperty("uuid")]
        public Guid UUID { get; private set; }

        [JsonProperty("process")]
        public FullyQualifiedName Process { get; private set; }

        [JsonProperty("date_invoke")]
        public DateTime DateInvoked { get; private set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(InvocationStatusConverter))]
        public InvocationStatus Status { get; private set; }
    }

    public class InvocationOutputConverter : JsonConverter<Invocation.Output>
    {
        public override Invocation.Output ReadJson(JsonReader reader, Type objectType, Invocation.Output existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);
            var status = Invocation.InvocationStatus.Unknown;
            Enum.TryParse(obj["status"].ToObject<string>(), true, out status); 
            var data = (JObject)obj["data"];
            Invocation.Output output = null;
            if (data.ContainsKey("text"))
            {
                var text = data["text"].ToObject<string>();
                output = new Invocation.TextOutput(status, text);
            }
            else
            {
                var progress = data["progress"].ToObject<float>();
                output = new Invocation.ProgressOutput(status, progress);
            }
            return output ?? new Invocation.Output(status);
        }

        public override void WriteJson(JsonWriter writer, Invocation.Output value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class InvocationStatusConverter : JsonConverter<Invocation.InvocationStatus>
    {
        public override Invocation.InvocationStatus ReadJson(JsonReader reader, Type objectType, Invocation.InvocationStatus existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = JValue.Load(reader).ToObject<string>();
            var status = Invocation.InvocationStatus.Unknown;
            Enum.TryParse(value, true, out status);
            return status;
        }

        public override void WriteJson(JsonWriter writer, Invocation.InvocationStatus value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

