using Microsoft.AspNetCore.Hosting;
using ServiceAPI.Dal;
using System;
using System.Threading.Tasks;

namespace ServiceAPI
{
    class Program
    {
        //Notare che all'avvio non viene inizializzato niente, per creare il DB, fare GET http://localhost:5000/api/setup in Postman
        static void Main(string[] args)
        {

            var host = new WebHostBuilder()
               .UseKestrel()
               .UseStartup<Startup>()
               .Build();

            Task restService = host.RunAsync();

            restService.Wait();
        }
    }
}
