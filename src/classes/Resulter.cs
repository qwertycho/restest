namespace restest
{
    public static class Resulter
    {
        public static void showResults(List<double> responseTimes, double duration)
        {
            int responses = responseTimes.Count();
            double average = responseTimes.Average();
            double RPS = Math.Round(responses / (duration / 1000), 1);

            Console.WriteLine("\n\n");	
            Console.WriteLine(" Total responses | Average response time | Responses per second");
            Console.WriteLine($" {responses, -15} | {average, -21} | {RPS, -21} \n");
        }
    }

}