
namespace restest
{
    public class Url
    {
        Settings settings = new Settings();

        private string url;
        public Url(string url)
        {
            this.url = chechUrl(url);
        }

        public string getUrl()
        {
            return url;
        }

        private string chechUrl(string url)
        {
            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return url;
            }
            else
            {
                
                return settings.getProtocol() + url;; 
            }
        }

    }
}