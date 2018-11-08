using System;
using System.Linq;
using System.Threading.Tasks;
using Vendita.MAS;

namespace Vendita.MAS.IntegrationTests
{
    class Program
    {
        static void Main(string[] args)
        {
            IApi api = new Api(Settings.serverURL);
            var task = Task.Run(async () => {
                await api.Authenticate();
                var invocations = await api.ListInvocationsAsync(90);
                Console.WriteLine(invocations.Length);
            });
            task.Wait();
        }
    }
}
