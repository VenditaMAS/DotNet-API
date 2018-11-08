using System;
namespace Vendita.MAS
{
    public abstract class ResourceRequest<Identifier, Method, Resource, Response> :
        IResourceRequest<Method, Resource, Response>,
        IIdentified
        where Resource : IResource, new() 
        where Method : IMethod, new()
    {
        public ResourceRequest(params Identifier[] identifiers)
        {
            if (identifiers.Length > 0)
            {
                Identifiers = String.Join(",", identifiers);
            }
        }

        public string Identifiers { get; private set; }
    }

    public class GetRequest<Identifier, Resource, Response> : ResourceRequest<Identifier, GET, Resource, Response>
        where Resource : IResource, new()
    {
        public GetRequest(params Identifier[] identifiers)
            : base(identifiers)
        {

        }

        public GetRequest()
            : this(new Identifier[0])
        {

        }
    }

    public class DeleteRequest<Identifier, Resource, Response> : ResourceRequest<Identifier, DELETE, Resource, Response>
        where Resource : IResource, new()
    {
        public DeleteRequest(params Identifier[] identifiers)
            : base(identifiers)
        {

        }
    }

}
