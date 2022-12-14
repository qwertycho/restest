using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace restest
{
    public class ResponseTest
    {
        private Resulter resulter = new Resulter();

        private HttpClient clientFactory()
        {
            //setting up the client for the http requests
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            return client;
        }

        public async Task TestResponseTimes(int duration, string url) 
        {
            HttpClient client = clientFactory();
            int errorCount = 0;

            //stopwatch to make sure the requests stop when the time is up
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<double> responseTimes= new List<double>();
            while(stopwatch.ElapsedMilliseconds < duration) 
            {
                
                if(errorCount > 10)
                {
                    Console.WriteLine("Too many errors, stopping");
                    break;
                }

                try
                {
                    Response response = await testResponseTime(client, url);
                    responseTimes.Add(response.responseTime);
                    Console.Write($"\r Got {response.responseCode} response in: {response.responseTime} MS ");

                }catch (Exception)
                {
                    Console.WriteLine($"Error in request to {url}");
                    errorCount++;
                }
            }

            if(responseTimes.Count > 0)
            {
                resulter.showResults(responseTimes, duration);
            }

        }

        public async Task testCountResponses(int count, string url)
        {
            HttpClient client = clientFactory();

            List<Task> tasks = new List<Task>();
            List<double> responseTimes = new List<double>();

            //making the selected amount of tasks
            for(int i = 0; i < count; i++)
            {
                tasks.Add(testResponseTime(client, url));
            }

            //start timing the tasks
            Stopwatch stopwatch = Stopwatch.StartNew();
            await Task.WhenAll(tasks);
            stopwatch.Stop();

            double elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
            resulter.showCountResults(responseTimes, count, elapsedTime);
        }

        private async Task<Response> testResponseTime(HttpClient client, string url)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            var serverResponse = await client.GetAsync(url);
            Response response = new Response();
            response.responseCode = (int)serverResponse.StatusCode;
            response.responseTime = stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Stop();

            return response;
        }

        internal class Response
        {
            public double responseTime { get; set; }
            public int responseCode { get; set; }
        }

    }

}
