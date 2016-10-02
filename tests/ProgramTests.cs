using System.IO;
using Xunit;

namespace FGet.Tests
{
    public class ProgramTests
    {
        public class Main
        {
            [Fact]
            public void CorrectUrlShouldSucceed()
            {
                FileInfo tempFile = new FileInfo(Path.GetTempFileName());
                int exitCode = Program.Main(new[] { "https://raw.githubusercontent.com/stbrenner/fget/master/README.md", "-o", tempFile.FullName });

                Assert.True(exitCode == 0);
                Assert.True(tempFile.Length > 30);
            }

            [Fact]
            public void MissingUrlShouldReturnExitCode1()
            {
                int exitCode = Program.Main(new string [] {});
                Assert.True(exitCode == 1);
            }
        }
    }
}
