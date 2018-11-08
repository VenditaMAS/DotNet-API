using System;
namespace Vendita.MAS
{
    public abstract class ResourceRequest<Method, Resource, Response> :
        IResourceRequest<Method, Resource, Response>,
        IIdentified
        where Resource : IResource, new() 
        where Method : IMethod, new()
    {
        public ResourceRequest(params object[] identifiers)
        {
            if (identifiers.Length > 0)
            {
                Identifiers = String.Join(",", identifiers);
            }
        }

        public string Identifiers { get; private set; }
    }

    public class GetRequest<Resource, Response> : ResourceRequest<GET, Resource, Response>
        where Resource : IResource, new()
    {
        public GetRequest(params object[] identifiers)
            : base(identifiers)
        {

        }

        public GetRequest()
            : this(new object[0])
        {

        }
    }

    public class DeleteRequest<Resource, Response> : ResourceRequest<DELETE, Resource, Response>
        where Resource : IResource, new()
    {
        public DeleteRequest(params object[] identifiers)
            : base(identifiers)
        {

        }
    }

}
