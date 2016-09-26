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
            IFileDownloader fileDownloader = new FileDownloader.FileDownloader();
            fileDownloader.DownloadFileCompleted += DownloadFileCompleted;

            if (args.Length < 1) return 1;
            fileDownloader.DownloadFileAsync(new Uri(args[0]), args[1]);

            completedEvent.WaitOne();
            return exitCode;
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
