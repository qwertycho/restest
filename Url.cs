
namespace restest
{
    public class Url
    {
        Settings settings = new Settings();

        private string url;
        public Url(string url)
        {
            this.url = checkUrl(url);
        }

        public string getUrl()
        {
            return this.url;
        }

        private string checkUrl(string url)
        {
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return url;
            }
            else
            {
                Console.WriteLine($"No protocol specified, using {settings.getProtocol()}");
                return settings.getProtocol() + url;; 
            }
        }

    }
}