using System;
using Newtonsoft.Json;

namespace Vendita.MAS.Models
{
    public class Invocation
    {
        [JsonProperty("uuid")]
        public Guid UUID { get; private set; }

        [JsonProperty("process")]
        public FullyQualifiedName Process { get; private set; }

        [JsonProperty("date_invoke")]
        public DateTime DateInvoked { get; private set; }

        [JsonProperty("status")]
        public string Status { get; private set; }
    }
}

