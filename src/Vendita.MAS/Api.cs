using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Vendita.MAS
{
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

        public async Task<Response> SendAsync<Method, Resource, Response>(IResourceRequest<Method, Resource, Response> request, int page) where Method : IMethod, new() where Resource : IResource, new()
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
            if (method.BodyRequired)
            {
                var stream = new MemoryStream();
                var writer = new StreamWriter(stream);
                var jsonWriter = new JsonTextWriter(writer);
                var serializer = new JsonSerializer()
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                };
                serializer.Serialize(jsonWriter, request);
                stream.Seek(0, SeekOrigin.Begin);
                requestMessage.Content = new StreamContent(stream);
                requestMessage.Content.Headers.Add("Content-Type", "application/json");
            }
            var responseMessage = await client.SendAsync(requestMessage);
            responseMessage.EnsureSuccessStatusCode();
            var raw = await responseMessage.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy(),
                }
            };
            return JsonConvert.DeserializeObject<Response>(raw, settings);
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
