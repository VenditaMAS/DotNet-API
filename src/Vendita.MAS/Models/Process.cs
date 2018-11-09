using System;
using Newtonsoft.Json;

namespace Vendita.MAS.Models
{
    public class Process
    {

        [JsonProperty("name")]        
        public FullyQualifiedName Name { get; private set; } 

        [JsonProperty("description")]
        public string Description { get; private set; }
    }
}