using System;
using System.Collections.Generic;

namespace Vendita.MAS
{
    public class InvocationRequest : GetRequest<Guid, Invocations, Envelope<Invocation>>,
        IParameterized
    {
        public InvocationRequest(DateTime dateInvoked, int period, params Guid[] identifiers)
            : base(identifiers)
        {
            Parameters["date_invoke"] = dateInvoked.ToString("yyyy-MM-dd");
            Parameters["period"] = $"{period}";
        }

        public IDictionary<string, string> Parameters { get; } = new Dictionary<string, string>(); 
    }
}