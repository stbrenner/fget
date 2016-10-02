using System;
using System.IO;
using Xunit;

namespace FGet.Tests
{
    class ConsoleDownloaderTests
    {
        public class Download
        {
            [Fact]
            public void CorrectUrlShouldSucceed()
            {
                var config = new Config()
                {
                    SourceUrl = new Uri("https://raw.githubusercontent.com/stbrenner/fget/master/README.md")
                };

                var tempFile = new FileInfo(Path.Combine(Path.GetTempPath(), "avira.exe"));
                if (tempFile.Exists)
                {
                    tempFile.Delete();
                }
                
                Environment.CurrentDirectory = Path.GetTempPath();

                var consoleDownloader = new ConsoleDownloader(config);
                consoleDownloader.Download();

                Assert.True(tempFile.Exists);
            }
        }
    }
}
