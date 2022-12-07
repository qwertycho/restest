using restest;

Console.WriteLine("Welcome to restest");

if(args.Length != 0)
{
    await parseArgs(args);
}   
else
{
    Console.WriteLine("No arguments given");
    Console.WriteLine("Usage: restest [mode] [seconds] [url] \n or H for help");
}

async Task parseArgs(string[] args)
{
    ResponseTest responseTest = new ResponseTest();

    switch(args[0])
    {
        case "H":
            Console.WriteLine("Commands: \n H - Help \n D - run with default settings \n M - run with custom settings");
            Console.WriteLine("Usage: restest [mode] [seconds] [url] ");
            break;

        case "D":
            Console.WriteLine("Running with default settings");
            Settings settings = new Settings();
            settings.checkSettings();
            Url url = new Url(settings.getUrl());
            await responseTest.TestResponseTimes(settings.getDuration(), url.getUrl());
            break;
        
        case "M":
            Console.WriteLine("Running with custom settings");
            Console.WriteLine($"Running for {args[1]} seconds");
            Console.WriteLine($"Running against {args[2]}");

            Url url2 = new Url(args[2]);
            int duration = int.Parse(args[1]) * 1000;

            await responseTest.TestResponseTimes(duration, url2.getUrl());
            break;
    }
}




