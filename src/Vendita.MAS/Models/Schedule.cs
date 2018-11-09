using System;
using Newtonsoft.Json;

namespace Vendita.MAS.Models
{
    using Vendita.MAS.Requests;
    using Vendita.MAS.Resources;

    public class Schedule: IResourceRequest<POST, Schedules, Envelope<Invocation>>
    {
        public Schedule(FullyQualifiedName process)
        {
            Process = process;
        }

        [JsonProperty("process")]
        public FullyQualifiedName Process { get; private set; }
        [JsonProperty("date_invoke")]
        public DateTime Date { get; private set; }

        public static implicit operator Schedule(string process)
        {
            return new Schedule(process);
        }

        public static implicit operator Schedule(FullyQualifiedName process)
        {
            return new Schedule(process);
        }
    }
}