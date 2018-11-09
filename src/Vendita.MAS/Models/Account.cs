using System;
using Newtonsoft.Json;

namespace Vendita.MAS.Models
{
    using Requests;
    using Resources;

    public class Account: 
        IResourceRequest<POST, Accounts, Envelope<Account>>,
        IResourceRequest<PATCH, Accounts, Envelope<Account>>
    {
        [JsonProperty("uuid")]
        public Guid UUID { get; private set; }
    }
}