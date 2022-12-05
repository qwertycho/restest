using restest;

Console.WriteLine("Welcome to restest");

Settings settings= new Settings();
settings.checkSettings();

ResponseTest ResponseTest= new ResponseTest();
await ResponseTest.TestResponseTimes(settings.getDuration(), settings.getUrl());