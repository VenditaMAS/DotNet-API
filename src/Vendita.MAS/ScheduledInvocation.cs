using System;
using Newtonsoft.Json;

namespace Vendita.MAS
{
    public class ScheduledInvocation: IResourceRequest<POST, Invocations, Envelope<Invocation>>
    {
        public ScheduledInvocation(FullyQualifiedName process)
        {
            Process = process;
        }

        [JsonProperty("process")]
        public FullyQualifiedName Process { get; private set; }
        [JsonProperty("date_invoke")]
        public DateTime Date { get; private set; }

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