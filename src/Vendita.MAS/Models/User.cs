using System;
using Newtonsoft.Json;

namespace Vendita.MAS.Models
{
    public class User
    {
        [JsonProperty("username")]
        public string UserName {get; private set;}
    }
}