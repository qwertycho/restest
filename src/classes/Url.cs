namespace restest
{
    public class Url
    {
        private string protocol;

        private string url;
        public Url(string url, string protocol)
        {
            this.protocol = protocol;
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
                Console.WriteLine($"No protocol specified, using {this.protocol}");
                return this.protocol + url; ;
            }
        }

    }
}