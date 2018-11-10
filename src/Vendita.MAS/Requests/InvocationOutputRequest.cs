using System;

namespace Vendita.MAS.Requests
{
    using Resources;
    using Models;

    public class InvocationOutputRequest : GetRequest<Guid, Invocations, Envelope<Invocation.Output>>,
        IChildResource
    {
        public InvocationOutputRequest(Guid identifier)
            : base(identifier)
        {
                        
        }

        public string ChildPath { get; } = "display";
    }
}