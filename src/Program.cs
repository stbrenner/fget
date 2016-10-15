using System;
using System.Reflection;

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

                if (config.Help)
                {
                    ShowHelp();
                    return 0;
                }

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

        private static void ShowHelp()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine(Resource.Help, version.Major, version.Minor);
        }
    }
}
