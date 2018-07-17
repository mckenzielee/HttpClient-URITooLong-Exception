using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ClientExceptionReproApp
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string hostUrl = $"http://{Environment.MachineName}:5000";
            var requestUri = $"api/fake/{new string('1', 30000)}";

            Task.Run(async () =>
            {
                Console.WriteLine($"Request URI Length: {requestUri.Length}");
                using (var client = new HttpClient() { BaseAddress = new Uri(hostUrl) })
                {
                    var requestCount = 0;
                    while (requestCount++ < 10)
                    {
                        try
                        {
                            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                            
                            using (var response = await client.SendAsync(request, CancellationToken.None))
                            {
                                await Console.Out.WriteLineAsync($"REQUEST {requestCount} - Response: {(int) response.StatusCode} {response.ReasonPhrase}");
                            }
                        }
                        catch (Exception ex)
                        {
                            await Console.Out.WriteLineAsync($"REQUEST {requestCount} - Exception: {ex}");
                        }
                    }
                }
            });


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
