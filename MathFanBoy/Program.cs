using Dapr.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MathFanBoy
{
    public class Operation
    {
        public int op1 { get; set; }
        public int op2 { get; set; }
    }
    class Program
    {
        static string[] endpoints = null;
        static string[] apis = new string[5] { "addition", "division", "multiplication", "substraction", "percentage" };
        static string[] operations = new string[5] { "addition/add", "division/divide", "multiplication/multiply", "substraction/substract", "percentage/percentage" };
        
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var runHostTask = host.RunAsync();

            var loopTask = Task.Run(async () =>
            {
                while (true)
                {
                    var pos = new Random().Next(0, 5);
                    using var client = new DaprClientBuilder().Build();
                    var operation = new Operation { op1 = 10, op2 = 2 };
                    try
                    {
                         var response = await client.InvokeMethodAsync<object, object>(
                         apis[pos], // The name of the Dapr application
                         operations[pos], // The method to invoke
                         operation); // The request payload                        
                        
                        Console.WriteLine(response);
                    }
                    catch(Exception ex) { 
                        Console.WriteLine(ex.ToString());
                    }
                    
                    await Task.Delay(5000);
                }
            });

            await Task.WhenAll(runHostTask, loopTask);

        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
    
}
