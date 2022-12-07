using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace restest
{
    public class Settings
    {
        private string defaultURL = "http://localhost:3000";
        private string defaultMS = "5";
        private string defaultProtocol = "http://";
        private string settingsName = AppDomain.CurrentDomain.BaseDirectory + "/" + "settings.json";

        public void checkSettings()
        {
            if (!File.Exists(settingsName))
            {
                Console.WriteLine($"No {settingsName} file found \n Setting up new defaults...");
                askDefaults();
            }
            else
            {
                loadSettings();
            }
        }

        public int getDuration()
        {
            return int.Parse(defaultMS);
        }

        public string getUrl()
        {
            return defaultURL;
        }

        public string getProtocol()
        {
            return defaultProtocol;
        }

        public void reset(){
            Console.WriteLine("Resetting default settings");
            askDefaults();
        }

        private void askDefaults()
        {

            string newDefaultURL = askDefaultUrl();
            string newDefaultDuration = askDefaultDuration();
            string newDefaultProtocol = askDefaultProtocol();

            saveSettings(newDefaultURL, newDefaultDuration, newDefaultProtocol);
        }

        private void loadSettings()
        {
            string jsonSettings = File.ReadAllText(settingsName);
            if(jsonSettings == null || jsonSettings == "") 
            {
                Console.WriteLine("Saved settings are empty!");
                askDefaults();
            }
            try
            {
                Defaults? settings = JsonSerializer.Deserialize<Defaults>(jsonSettings);
                defaultURL = settings.Url;
                defaultMS = settings.MS;
                defaultProtocol = settings.protocol;

            }catch(Exception ex)
            {
                Console.WriteLine("Error while reading saved settings!");
                askDefaults();
            }
        }

        private string askDefaultUrl()
        {
            Console.WriteLine($"Enter default URL | leave empty for {defaultURL}");
            string? input = Console.ReadLine();
            if (input == "") { input = defaultURL; }
            Console.WriteLine(input);
            Url url = new Url(input);

            Console.WriteLine($"Using {url.getUrl()} as default URL");
            string? newDefaultURL = url.getUrl();
            if (newDefaultURL == "") { newDefaultURL = defaultURL; }
            return newDefaultURL;
        }

        private string askDefaultDuration()
        {
            Console.WriteLine($"Enter default duration | leave empty for {defaultMS}");
            string? newDefaultDuration = Console.ReadLine();
            newDefaultDuration = parseDuration( newDefaultDuration);
            return newDefaultDuration;
        }

        private string askDefaultProtocol()
        {
            Console.WriteLine($"Enter default protocol | 1 for http, 2 for https | leave empty for http");
            string? choice = Console.ReadLine();
            if (choice == "2")
                { 
                    return "https://"; 
                } else
                {
                    return "http://";
                }
        }

        private string parseDuration(string? duration)
        {
            if (duration == "" || duration == null) { duration = defaultMS; }
            try
            {
                int MS = int.Parse(duration);
                MS = MS * 1000;
                return MS.ToString();
            } catch 
            {
                int MS = int.Parse(duration);
                MS = MS * 1000;
                return MS.ToString();
            }
        }

        private void saveSettings(string newURL, string newDuration, string protocol)
        {
            Defaults defaults = new Defaults
            {
                Url = newURL,
                MS = newDuration,
                protocol = protocol
            };

            try
            {
                string json = JsonSerializer.Serialize(defaults);
                File.WriteAllText(settingsName, json);
                loadSettings();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal class Defaults{
            public string? Url { get; set; }
            public string? MS { get; set; }
            public string protocol { get; set; } = "http";
        }
    }
}
