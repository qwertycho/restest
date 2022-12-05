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

        public async Task TestResponseTimes(int duration, string url) 
        {

            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            Stopwatch stopwatch = Stopwatch.StartNew();
            List<double> responseTimes= new List<double>();

            while(stopwatch.ElapsedMilliseconds < duration) 
            {
                try
                {
                    double resTime = await testResponseTime(client, url);
                    responseTimes.Add(resTime);
                    Console.WriteLine($"Got response in : {resTime}");

                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            showResults(responseTimes, duration);

        }

        private async Task<double> testResponseTime(HttpClient client, string url)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var response = await client.GetAsync(url);
            stopwatch.Stop();

            double elapsedTime = stopwatch.Elapsed.TotalMilliseconds;
            return elapsedTime;
        }

        private void showResults(List<double> responseTimes, int duration)
        {
            int responses = responseTimes.Count();
            double average = Math.Round(responseTimes.Average(), 2);
            double responsesPerSecond = responses / (duration / 1000);
            double RPS = Math.Round(responsesPerSecond, 2);
            Console.WriteLine($"{responses} responses in {duration} MS");
            Console.WriteLine($"{average} average response time");
            Console.WriteLine($"{RPS} responses per second");
        }

    }


}
