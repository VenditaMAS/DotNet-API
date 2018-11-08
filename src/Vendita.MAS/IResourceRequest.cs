using System;
namespace Vendita.MAS
{
    public interface IResourceRequest<Method, Resource, Response> where Method: IMethod, new() where Resource : IResource, new()
    {

    }
}
