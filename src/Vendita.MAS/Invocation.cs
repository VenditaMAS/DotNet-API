using System;
using Newtonsoft.Json;

namespace Vendita.MAS
{
    public class Invocation
    {
        [JsonProperty("uuid")]
        public Guid UUID { get; private set; }
    }
}

