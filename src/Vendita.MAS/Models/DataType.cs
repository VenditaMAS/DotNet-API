using System;
using Newtonsoft.Json;

namespace Vendita.MAS.Models
{
    public class DataType
    {
        [JsonProperty("name")]
        public string Name { get; private set; }
    }
}