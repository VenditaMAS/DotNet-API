using System;
using System.Linq;
using System.Threading.Tasks;

namespace Vendita.MAS.Primitives
{
    using Vendita.MAS.Models;
    using Vendita.MAS.Requests;
    using Vendita.MAS.Resources;

    public static class ApiExtensions
    {
        public static async Task<T[]> SendAsync<Method, Resource, T>(this IApi @this, IResourceRequest<Method, Resource, Envelope<T>> request)
            where Method: IMethod, new()
            where Resource: IResource, new()
        {
            var envelope = await @this.SendAsync(request, 1);
            if (envelope.PageCount <= 1) return envelope.Contents;
            var tasks = (from page in Enumerable.Range(2, envelope.PageCount - 1) select @this.SendAsync(request, page)).ToList();
            tasks.Insert(0, Task.FromResult(envelope));
            await Task.WhenAll(tasks.ToArray());
            return tasks.Select(task => task.Result.Contents).SelectMany(a => a).ToArray();
        }

        public static async Task<T> FirstAsync<Method, Resource, T>(this IApi @this, IResourceRequest<Method, Resource, Envelope<T>> request)
            where Method: IMethod, new()
            where Resource: IResource, new()
        {
            var envelope = await @this.SendAsync(request, 1);
            return envelope.Contents.First();
        }

        public static Task<T[]> ListAsync<Resource, T>(this IApi @this, IResourceRequest<GET, Resource, Envelope<T>> request)
            where Resource: IResource, new()
        {
            return @this.SendAsync(request);
        }

        public static Task<T> GetAsync<Resource, T>(this IApi @this, IResourceRequest<GET, Resource, Envelope<T>> request)
            where Resource: IResource, new()
        {
            return @this.FirstAsync(request);
        }

        public static Task<T[]> ListAsync<Identifier, Resource, T>(this IApi @this, params Identifier[] identifiers)
            where Resource: IResource, new()
        {
            return @this.ListAsync(new GetRequest<Identifier, Resource, Envelope<T>>(identifiers));
        }

        public static Task<T> GetAsync<Identifier, Resource, T>(this IApi @this, Identifier identifier)
            where Resource: IResource, new()
        {
            return @this.FirstAsync(new GetRequest<Identifier, Resource, Envelope<T>>(identifier));
        }

        public static Task DeleteAsync<Identifier, Resource>(this IApi @this, params Identifier[] identifiers)
            where Resource: IResource, new()
        {
            if (identifiers.Length == 0) return Task.FromResult(true);
            return @this.SendAsync(new DeleteRequest<Identifier, Resource, Envelope<object>>(identifiers), 1);
        }

    }
}