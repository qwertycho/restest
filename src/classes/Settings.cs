using System.Text.Json;

namespace restest
{
    public class Settings
    {
        //Program default setting
        private string defaultURL = "http://localhost:3000";
        private string defaultMS = "5";
        private string defaultProtocol = "http://";

        //Making sure that the settings.json file will be in the same directory as the program
        private string settingsName = AppDomain.CurrentDomain.BaseDirectory + "/" + "settings.json";

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

        public void reset(){
            Console.WriteLine("Resetting default settings");
            askDefaults();
        }

        private void askDefaults()
        {
            string newDefaultProtocol = askDefaultProtocol();
            string newDefaultURL = askDefaultUrl();
            string newDefaultDuration = askDefaultDuration();

            saveSettings(newDefaultURL, newDefaultDuration, newDefaultProtocol);

            showSettings();

            Console.WriteLine("Settings saved!");
        }

        private void loadSettings()
        {
            string jsonSettings = File.ReadAllText(settingsName);
            if(jsonSettings == null || jsonSettings == "") 
            {
                Console.WriteLine("Saved settings are empty!");
                askDefaults();
            } else
            {
                Defaults defaults = JsonSerializer.Deserialize<Defaults>(jsonSettings) ?? new Defaults();
                parseSettings(defaults);
            }
        }

        private void parseSettings(Defaults defaults)
        {
            defaultURL = defaults.Url ?? defaultURL;
            defaultMS = defaults.MS ?? defaultMS;
            defaultProtocol = defaults.protocol ?? defaultProtocol;
        }

        private string askDefaultProtocol()
        {
            Console.WriteLine($"Enter default protocol | 1 for http, 2 for https | leave empty for http");
            string? choice = Console.ReadLine();
            if (choice == "2")
            {
                defaultProtocol = "https://";
                return "https://";
            }
            else
            {
                defaultProtocol = "http://";
                return "http://";
            }
        }

        private string askDefaultUrl()
        {
            Console.WriteLine($"Enter default URL | leave empty for {defaultURL}");
            string? input = Console.ReadLine();
            if (input == "") { input = defaultURL; }
            Console.WriteLine(input);
            Url url = new Url(input ?? defaultURL, this.defaultProtocol);

            //using Url the check if the url is correct and set http protocol if it's missing
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

        private string parseDuration(string? duration)
        {
            //making sure that there is a value
            if (duration == "" || duration == null) { duration = defaultMS; }
            //try to parse seconds to MS
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

        private void showSettings()
        {
            Console.WriteLine("Settings:");
            Console.WriteLine($" {defaultURL} | {defaultMS} | {defaultProtocol}");
        }

        //class for creating the json
        internal class Defaults{
            public string? Url { get; set; }
            public string? MS { get; set; }
            public string protocol { get; set; } = "http";
        }
    }
}
