using System;
using System.Linq;
using System.Threading.Tasks;
using Vendita.MAS;
using Vendita.MAS.Models;

namespace Vendita.MAS.IntegrationTests
{
    class Program
    {
        static void Main(string[] args)
        {
            IApi api = new Api(Settings.serverURL);
            var task = Task.Run(async () => {
                var schedule = new Schedule("vendita.test_display");
                await api.PostAsync(schedule);
            });
            task.Wait();
        }
    }
}

