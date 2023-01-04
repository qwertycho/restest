namespace restest
{

    public class Resulter
    {

        public void showResults(List<double> responseTimes, int duration)
        {
            int responses = responseTimes.Count();
            double average = Math.Round(responseTimes.Average(), 2);
            double responsesPerSecond = responses / (duration / 1000);
            double RPS = Math.Round(responsesPerSecond, 2);

            Console.WriteLine("\n\n");	
            Console.WriteLine(" Total responses | Average response time | Responses per second");
            Console.WriteLine($" {responses, -15} | {average, -21} | {RPS, -21}");
        }

        public void showCountResults(List<double> responseTimes, int count, double elapsedTime)
        {
            Console.WriteLine($"{count} responses in {elapsedTime} MS");
        }

    }

}