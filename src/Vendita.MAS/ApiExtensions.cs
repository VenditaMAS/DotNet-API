using System;
using System.Linq;
using System.Threading.Tasks;

namespace Vendita.MAS
{
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
            return @this.ListAsync(new GetRequest<Resource, Envelope<T>, Identifier>(identifiers));
        }

        public static Task<T> GetAsync<Identifier, Resource, T>(this IApi @this, Identifier identifier)
            where Resource: IResource, new()
        {
            return @this.FirstAsync(new GetRequest<Resource, Envelope<T>, Identifier>(identifier));
        }

        public static Task DeleteAsync<Identifier, Resource>(this IApi @this, params Identifier[] identifiers)
            where Resource: IResource, new()
        {
            if (identifiers.Length == 0) return Task.FromResult(true);
            return @this.FirstAsync(new DeleteRequest<Resource, Envelope<object>, Identifier>(identifiers));
        }

        public static Task<T> PostAsync<Resource, T>(this IApi @this, IResourceRequest<POST, Resource, Envelope<T>> request)
            where Resource: IResource, new()
        {
            return @this.FirstAsync(request);
        }

        public static Task<T> PatchAsync<Resource, T>(this IApi @this, IResourceRequest<PATCH, Resource, Envelope<T>> request)
            where Resource: IResource, new()
        {
            return @this.FirstAsync(request);
        }

        public static Task<T> PutAsync<Resource, T>(this IApi @this, IResourceRequest<PUT, Resource, Envelope<T>> request)
            where Resource: IResource, new()
        {
            return @this.FirstAsync(request);
        }

        public static Task<Form[]> ListFormsAsync(this IApi @this, params Guid[] identifiers)
        {
            return @this.ListAsync<Guid, FormResource, Form>(identifiers);
        }

        public static Task DeleteFormsAsync(this IApi @this, params Guid[] identifiers)
        {
            return @this.DeleteAsync<Guid, FormResource>(identifiers);
        }

        public static Task<Invocation[]> ListInvocationsAsync(this IApi @this, params Guid[] identifiers)
        {
            return @this.ListAsync<Guid, InvocationResource, Invocation>(identifiers);
        }

    }
}
