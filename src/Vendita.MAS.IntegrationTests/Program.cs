using System;
using System.Threading.Tasks;
using Vendita.MAS;

namespace Vendita.MAS.IntegrationTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Run(async () => {
                var api = new Api("https://user:password@mascloud3.venditabeta.com:443/mas");
                var invocations = await api.ListInvocationsAsync();
                Console.WriteLine(invocations.Length);
            });
            task.Wait();
        }
    }
}
