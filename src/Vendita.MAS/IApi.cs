using System;
using System.Threading.Tasks;

namespace Vendita.MAS
{
    public interface IApi
    {
        Task<Response> SendAsync<Method, Resource, Response>(IResourceRequest<Method, Resource, Response> request, int page) where Method : IMethod, new() where Resource : IResource, new();
    }

    public static class IApiExtensions
    {

    }
}
