using System;
using System.Net.Http;

namespace Vendita.MAS.Requests
{
    public interface IMethod
    {
        HttpMethod Method { get; }
        bool BodyRequired { get; }
    }

    public class GET: IMethod
    {
        public HttpMethod Method { get; } = HttpMethod.Get;
        public bool BodyRequired { get; } = false;
    }

    public class DELETE : IMethod
    {
        public HttpMethod Method { get; } = HttpMethod.Delete;
        public bool BodyRequired { get; } = false;
    }

    public class POST: IMethod
    {
        public HttpMethod Method { get; } = HttpMethod.Post;
        public bool BodyRequired { get; } = true;
    }

    public class PUT: IMethod
    {
        public HttpMethod Method { get; } = HttpMethod.Put;
        public bool BodyRequired { get; } = true;
    }

    public class PATCH: IMethod
    {
        public HttpMethod Method { get; } = new HttpMethod("PATCH");
        public bool BodyRequired { get; } = true;
    }
}
