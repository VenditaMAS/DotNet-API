using System;
namespace Vendita.MAS
{
    /// <summary>
    /// The interface implemented by types representing
    /// resource requests.
    /// </summary>
    /// <typeparam name="Method">The HTTP method to use.</typeparam>
    /// <typeparam name="Resource">The MAS resource to request.</typeparam>
    /// <typeparam name="Response">The type of the response.</typeparam>
    public interface IResourceRequest<Method, Resource, Response>
        where Method: IMethod, new()
        where Resource : IResource, new()
    {

    }
}
