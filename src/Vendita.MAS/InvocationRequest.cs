using System;
using System.Collections.Generic;

namespace Vendita.MAS
{
    public class InvocationRequest : GetRequest<Guid, Invocations, Envelope<Invocation>>,
        IParameterized
    {
        public IDictionary<string, string> Parameters => throw new NotImplementedException();
    }
}