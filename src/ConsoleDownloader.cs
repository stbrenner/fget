using FileDownloader;
using System;
using System.Threading;

namespace FGet
{
    internal class ConsoleDownloader
    {
        private AutoResetEvent completedEvent = new AutoResetEvent(false);
        private readonly Config config;
        private bool succeeded;
        private DateTime lastProgressUpdate;

        public ConsoleDownloader(Config config)
        {
            this.config = config;
        }

        /// <returns>True, if succeeded.</returns>
        public bool Download()
        {
            ValidateSourceUrl();

            IFileDownloader fileDownloader = new FileDownloader.FileDownloader();
            fileDownloader.DownloadFileCompleted += DownloadFileCompleted;
            fileDownloader.DownloadProgressChanged += DownloadProgressChanged;

            StartDownload(fileDownloader);

            completedEvent.WaitOne();
            return succeeded;
        }

        private void ValidateSourceUrl()
        {
            if (config.SourceUrl == null)
            {
                throw new Exception("Source URL is missing.");
            }
        }

        private void StartDownload(IFileDownloader fileDownloader)
        {
            Console.WriteLine("Downloading...");

            if (string.IsNullOrEmpty(config.TargetPath))
            {
                fileDownloader.DownloadFileAsyncPreserveServerFileName(config.SourceUrl, Environment.CurrentDirectory);
            }
            else
            {
                fileDownloader.DownloadFileAsync(config.SourceUrl, config.TargetPath);
            }
        }

        private void DownloadProgressChanged(object sender, DownloadFileProgressChangedArgs eventArgs)
        {
            lock (this)
            {
                TimeSpan durationSinceLastUpdate = (DateTime.Now - lastProgressUpdate).Duration();
                if (durationSinceLastUpdate < TimeSpan.FromSeconds(1)) return;

                Console.CursorLeft = 0;
                Console.Write("{0}%", eventArgs.ProgressPercentage);
                lastProgressUpdate = DateTime.Now;
            }
        }

        private void DownloadFileCompleted(object sender, DownloadFileCompletedArgs eventArgs)
        {
            Console.CursorLeft = 0;

            if (eventArgs.State == CompletedState.Succeeded)
            {
                succeeded = true;
                Console.WriteLine("Download succeeded");
            }
            else if (eventArgs.State == CompletedState.Failed)
            {
                succeeded = false;
                Console.WriteLine("Download failed");
            }

            completedEvent.Set();
        }
    }
}
