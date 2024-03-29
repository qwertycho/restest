﻿using System.Net.Http.Headers;
using System.Diagnostics;

namespace restest
{
    public class ResponseTest
    {
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

                }catch (Exception e)
                {
                    Console.WriteLine($" \n Error in request to {url}, error: {e.Message}");
                    errorCount++;
                }
            }

            if(responseTimes.Count > 0)
            {
                Resulter.showResults(responseTimes, duration);
            }

        }

        public async Task testCountResponses(int count, string url)
        {
            HttpClient client = clientFactory();

            List<double> responseTimes = new List<double>();
            List<Response> responses = new List<Response>();

            Stopwatch stopwatch = Stopwatch.StartNew();
            double FirstTimeOfset = 0;
            for (int i = 0; i < count +1 ; i++)
            {
                Response res = await testResponseTime(client, url);
                if(i != 0){

                responseTimes.Add(res.responseTime);
                } else{
                    FirstTimeOfset = res.responseTime;
                }
            }

            double elapsedTime = stopwatch.Elapsed.TotalMilliseconds - FirstTimeOfset;
            stopwatch.Stop();

            Resulter.showResults(responseTimes, elapsedTime);
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
