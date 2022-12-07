using restest;

Console.WriteLine("Welcome to restest");

if(args.Length == 0){
    Console.WriteLine("No arguments, use H for help");
} else if(args[0] == "D"){
    Settings settings= new Settings();
    settings.checkSettings();

    ResponseTest ResponseTest= new ResponseTest();
    await ResponseTest.TestResponseTimes(settings.getDuration(), settings.getUrl());
} else if(args[0] == "M"){
    ResponseTest ResponseTest= new ResponseTest();
    int MS = int.Parse(args[1]) * 1000;
    string url = args[2];
    await ResponseTest.TestResponseTimes(MS, url);
} else if(args[0] == "H"){
        Console.WriteLine("D = default settings");
        Console.WriteLine("M = manual settings, time in seconds, url");
        Console.WriteLine("H = help");
} else{
        Console.WriteLine("Invalid argument, use H for help");
}


