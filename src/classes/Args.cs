namespace restest
{

    public class Args
    {
        private string[] args;

        public Args(string[] args)
        {
            this.args = args;
            this.testArgs();
        }

        private void testArgs()
        {
                this.ModeSet();
                this.IntSet();
                this.UrlSet();
        }
        private void correctAmountOfArgs()
        {
           if(args.Length < 3)
           {
            throw new Exception("Not enough arguments set!");
           }
        }
        private void ModeSet()
        {
            if (args[0] != "H" || args[0] != "T" || args[0] != "C")
            {
                throw new Exception("No correct mode set!");
            }
        }

        private void IntSet()
        {
            try
            {
                int.Parse(args[1]);
            }
            catch
            {
                throw new Exception("No integers set form time/count!");
            }
        }

        private void UrlSet()
        {
            if (args[2] != "" && args[2] != null && args[2] != " ")
            {
                throw new Exception("No url specified!");
            }

        }
    }

}