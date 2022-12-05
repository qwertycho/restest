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
        private string settingsName = "./settings.json";

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

        private void askDefaults()
        {

            string newDefaultURL = askDefaultUrl();
            string newDefaultDuration = askDefaultDuration();

            saveSettings(newDefaultURL, newDefaultDuration);
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

            }catch(Exception ex)
            {
                Console.WriteLine("Error while reading saved settings!");
                askDefaults();
            }
        }

        private string askDefaultUrl()
        {
            Console.WriteLine($"Enter default URL | leave empty for {defaultURL}");
            string? newDefaultURL = Console.ReadLine() ?? defaultURL;
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

        private void saveSettings(string newURL, string newDuration)
        {
            Defaults defaults = new Defaults
            {
                Url = newURL,
                MS = newDuration
            };

            try
            {
                string json = JsonSerializer.Serialize(defaults);
                File.WriteAllText("./" + settingsName, json);
                loadSettings();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        internal class Defaults{
            public string? Url { get; set; }
            public string? MS { get; set; }
        }
    }

}
