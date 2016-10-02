using System;

namespace FGet
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var argumentParser = new ArgumentParser(args);
                Config config = argumentParser.Parse();

                var consoleDownloader = new ConsoleDownloader(config);
                if (!consoleDownloader.Download())
                {
                    return 2;   // Download failed
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error: {0}", e.Message);
                return 1;
            }
        }
    }
}
