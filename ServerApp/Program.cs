using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading;

namespace ServerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();

            System.Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cancellationToken.Cancel();
            };

            IWebHost host = new WebHostBuilder()
                     .UseUrls($"http://{Environment.MachineName}:5000")
                     .UseKestrel()
                     .UseStartup<Startup>()
                     .Build();


            host.RunAsync(cancellationToken.Token).GetAwaiter().GetResult();

        }
    }
}
