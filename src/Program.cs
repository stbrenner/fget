using FileDownloader;
using System;
using System.Threading;

namespace FGet
{
    public class Program
    {
        private static AutoResetEvent completedEvent = new AutoResetEvent(false);
        private static int exitCode;

        public static int Main(string[] args)
        {
            try
            {
                var argumentParser = new ArgumentParser(args);
                Config config = argumentParser.Parse();

                IFileDownloader fileDownloader = new FileDownloader.FileDownloader();
                fileDownloader.DownloadFileCompleted += DownloadFileCompleted;
                fileDownloader.DownloadFileAsync(config.SourceUrl, config.TargetPath);

                completedEvent.WaitOne();
                return exitCode;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error: {0}", e.Message);
                return 1;
            }
        }

        private static void DownloadFileCompleted(object sender, DownloadFileCompletedArgs eventArgs)
        {
            if (eventArgs.State == CompletedState.Succeeded)
            {
                exitCode = 0;
                Console.WriteLine("Download completed");
            }
            else if (eventArgs.State == CompletedState.Failed)
            {
                exitCode = 2;
                Console.WriteLine("Download failed");
            }

            completedEvent.Set();
        }
    }
}
