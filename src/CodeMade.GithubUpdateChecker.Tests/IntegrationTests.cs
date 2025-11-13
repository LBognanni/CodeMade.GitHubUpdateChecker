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
            var checker = VersionChecker.Create(
                "LBognanni", 
                "ImageViewer", 
                new Version(0, 0, 0), 
                "Image Viewer");
            checker.TempData.Clear();
            await checker.NotifyIfNewVersion();
            await Task.Delay(TimeSpan.FromSeconds(10));
        }

        [Ignore("Local development only")]
        [Test]
        public async Task Test_WithRealData2()
        {
            var checker = VersionChecker.Create(
                "LBognanni", 
                "CodeMadeClock", 
                new Version(1, 3, 7), 
                "CodeMade Clock",
                "v",
                v => $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}");
            checker.TempData.Clear();
            await checker.NotifyIfNewVersion();
            await Task.Delay(TimeSpan.FromSeconds(10));
        }
    }
}
