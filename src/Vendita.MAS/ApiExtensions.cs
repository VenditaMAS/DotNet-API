using System;
using System.Linq;
using System.Threading.Tasks;

namespace Vendita.MAS
{
    using Models;
    using Primitives;
    using Requests;
    using Resources;

    public static class ApiExtensions
    {
        public static Task Authenticate(this IApi @this)
        {
            return @this.SendAsync(new AuthenticationRequest(), 1);
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
            return @this.ListAsync<Guid, Forms, Form>(identifiers);
        }

        public static Task DeleteFormsAsync(this IApi @this, params Guid[] identifiers)
        {
            return @this.DeleteAsync<Guid, Forms>(identifiers);
        }

        public static Task<Invocation[]> ListInvocationsAsync(this IApi @this, DateTime dateInvoked, int period, params Guid[] identifiers)
        {
            return @this.ListAsync(new InvocationRequest(dateInvoked, period, identifiers)); 
        }

        public static Task<Invocation[]> ListInvocationsAsync(this IApi @this, int daysAgo, params Guid[] identifiers)
        {
            var dateInvoked = DateTime.UtcNow.AddDays((double)daysAgo * -1); 
            return @this.ListInvocationsAsync(dateInvoked, daysAgo);
        }
    }
}
