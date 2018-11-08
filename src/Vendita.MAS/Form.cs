using System;
using Newtonsoft.Json;

namespace Vendita.MAS
{
    public class Form
    {
        [JsonProperty("uuid")]
        public Guid UUID { get; private set; }
        [JsonProperty("name")]
        public string Name { get; private set; }
    }
}
