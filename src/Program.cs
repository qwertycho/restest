using restest;

await parseArgs(args);

async Task parseArgs(string[] args)
{
    ResponseTest responseTest = new ResponseTest();
    Settings settings = new Settings();

    switch(args[0])
    {
        case "D":
            Console.WriteLine("Running with default settings");
            settings.checkSettings();
            Url url = new Url(settings.getUrl(), settings.getProtocol());
            await responseTest.TestResponseTimes(settings.getDuration(), url.getUrl());
            break;
        
        case "T":
            Console.WriteLine("Running in Time mode");
            Console.WriteLine($"Running for {args[1]} seconds");
            Console.WriteLine($"Running against {args[2]}");

            Url url2 = new Url(args[2], settings.getProtocol());
            int duration = int.Parse(args[1]) * 1000;

            await responseTest.TestResponseTimes(duration, url2.getUrl());
            break;

        case "C":
            Url url3 = new Url(args[2], settings.getProtocol());
            int count = int.Parse(args[1]);

            Console.WriteLine("Running in Count mode");
            Console.WriteLine($"Running for {args[1]} requests");
            Console.WriteLine($"Running against {url3.getUrl()}");
            await responseTest.testCountResponses(count, url3.getUrl());
            break;

        case "R":
            settings.reset();
            break;

        case "H":
            Console.WriteLine("Commands: \n H - Help \n D - run with default settings \n T - run timed test with custom settings \n C - send set amount of requests \n R reset defaults");
            Console.WriteLine("Usage: restest [mode] [seconds / count] [url] ");
            break;
        
        default:
            Console.WriteLine("Invalid command");
            Console.WriteLine("Usage: restest [mode] [seconds / count] [url] \n or H for help");
            break;
    }
}