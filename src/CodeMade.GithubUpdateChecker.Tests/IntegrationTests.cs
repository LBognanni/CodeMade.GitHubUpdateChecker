using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CodeMade.GithubUpdateChecker.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Ignore("Local development only")]
        [Test]
        public async Task Test_WithRealData()
        {
            var checker = VersionChecker.Create("LBognanni", "ImageViewer", new Version(0, 0, 0), "Image Viewer");
            await checker.NotifyIfNewVersion();
        }

        [Ignore("Local development only")]
        [Test]
        public async Task Test_WithRealData2()
        {
            var checker = VersionChecker.Create("LBognanni", "CodeMadeClock", new Version(1, 3, 7), "CodeMade Clock", "v");
            await checker.NotifyIfNewVersion();
        }
    }
}
