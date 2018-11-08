using System;
using Newtonsoft.Json;

namespace Vendita.MAS
{
    public class Invocation
    {
        [JsonProperty("uuid")]
        public Guid UUID { get; private set; }

        [JsonProperty("process")]
        public FullyQualifiedName Process { get; private set; }

        [JsonProperty("date_invoke")]
        public DateTime DateInvoked { get; private set; }
    }
}

