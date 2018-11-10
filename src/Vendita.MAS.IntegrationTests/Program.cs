using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vendita.MAS;
using Vendita.MAS.Models;
using Vendita.MAS.Requests;
using Vendita.MAS.Resources;

namespace Vendita.MAS.IntegrationTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Run(async () => {
                IApi api = new Api(Settings.serverURL);
                await api.PostAsync(new ScheduledInvocation("vendita.test_display", date: DateTime.UtcNow.AddSeconds(20)));
            });
            task.Wait();
        }
    }
}

