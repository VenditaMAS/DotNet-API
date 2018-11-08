using System;
namespace Vendita.MAS
{
    public abstract class ResourceRequest<Method, Resource, Response, Identifier> :
        IResourceRequest<Method, Resource, Response>,
        IIdentified
        where Resource : IResource, new() where Method : IMethod, new()
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

    public class GetRequest<Resource, Response, Identifier> : ResourceRequest<GET, Resource, Response, Identifier>
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

    public class DeleteRequest<Resource, Response, Identifier> : ResourceRequest<DELETE, Resource, Response, Identifier>
        where Resource : IResource, new()
    {
        public DeleteRequest(params Identifier[] identifiers)
            : base(identifiers)
        {

        }
    }

}
