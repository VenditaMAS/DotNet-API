using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Vendita.MAS.Models
{
    using Vendita.MAS.Requests;
    using Vendita.MAS.Resources;

    public class ScheduledInvocation: IResourceRequest<POST, Invocations, Envelope<Invocation>>
    {
        public ScheduledInvocation(FullyQualifiedName process, Dictionary<String, JValue> parameters = null, DateTime? date = null)
        {
            Process = process;
            Parameters = parameters ?? new Dictionary<string, JValue>();
            Date = date;
        }

        [JsonProperty("name")]
        public FullyQualifiedName Process { get; private set; }

        [JsonProperty("date_invoke")]
        public DateTime? Date { get; private set; }
        
        [JsonProperty("parameters")]
        public Dictionary<String, JValue> Parameters { get; private set; } = new Dictionary<String, JValue>();

        public static implicit operator ScheduledInvocation(string process)
        {
            return new ScheduledInvocation(process);
        }

        public static implicit operator ScheduledInvocation(FullyQualifiedName process)
        {
            return new ScheduledInvocation(process);
        }
    }
}