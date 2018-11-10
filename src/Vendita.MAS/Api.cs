using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vendita.MAS
{
    using Requests;
    using Resources;

    public class Api: IApi, IDisposable
    {
        private readonly HttpClient client = new HttpClient();

        public Api(Uri baseAddress)
        {
            var builder = new UriBuilder(baseAddress);
            var username = builder.UserName;
            var password = builder.Password;
            builder.UserName = null;
            builder.Password = null;
            client.BaseAddress = builder.Uri;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(bytes)}");
        }

        public Api(string baseAddress)
            : this(new Uri(baseAddress))
        {

        }

        public async Task<Response> SendAsync<Method, Resource, Response>(IResourceRequest<Method, Resource, Response> request, int page)
            where Method: IMethod, new()
            where Resource: IResource, new()
        {
            var resource = new Resource();
            var path = new UriBuilder(client.BaseAddress).Path;
            Uri uri;
            if (path == null)
            {
                uri = new Uri(client.BaseAddress, new Uri(resource.Path, UriKind.Relative));
            }
            else
            {
                uri = new Uri(client.BaseAddress, new Uri($"{path}/{resource.Path}", UriKind.Relative));
            }
            var builder = new UriBuilder(uri);
            if (request is IIdentified identified)
            {
                if (identified.Identifiers != null)
                {
                    builder.Path += $"/{identified.Identifiers}";
                }
            }
            if (request is IChildResource child)
            {
                builder.Path += $"/{child.ChildPath}";
            }
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            if (request is IParameterized parameterized)
            {
                parameters = parameterized.Parameters;
            }
            var method = new Method();
            if (method is GET)
            {
                parameters["page"] = $"{page}";
                parameters["page_size"] = "3500";
                if (resource is IPagedResource paged)
                {
                    parameters["page_size"] = $"{paged.PageSize}";
                }
            }
            builder.Query = String.Join("&", from p in parameters select $"{p.Key}={p.Value}");
            var requestMessage = new HttpRequestMessage(method.Method, builder.Uri);
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy(),
                },
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss"
            };
            if (method.BodyRequired)
            {
                var up = JsonConvert.SerializeObject(request, settings);
                Debug.WriteLine(up);
                requestMessage.Content = new StringContent(up, Encoding.UTF8, "application/json");
            }
            var responseMessage = await client.SendAsync(requestMessage);
            var down = await responseMessage.Content.ReadAsStringAsync();
            Debug.WriteLine(down);
            responseMessage.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Response>(down, settings);
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
