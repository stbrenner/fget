using Xunit;

namespace FGet.Tests
{
    public class ArgumentParserTests
    {
        public class Parse
        {
            [Fact]
            public void OnlyUrlShouldBeEnough()
            {
                var argumentParser = new ArgumentParser(new[] { "http://test/" });
                Config config = argumentParser.Parse();

                Assert.True(config.SourceUrl.AbsoluteUri == "http://test/");
            }

            [Fact]
            public void TargetPathShouldSucceed()
            {
                var argumentParser = new ArgumentParser(new[] { "http://source/", "-o", "c:\target" });
                Config config = argumentParser.Parse();

                Assert.True(config.SourceUrl.AbsoluteUri == "http://source/");
                Assert.True(config.TargetPath == "c:\target");
            }

            [Fact]
            public void NoArgumentShouldLeadToEmptyConfig()
            {
                var argumentParser = new ArgumentParser(new string[] {});
                Config config = argumentParser.Parse();

                Assert.True(config.SourceUrl == null);
                Assert.True(config.TargetPath == null);
            }
        }
    }
}
