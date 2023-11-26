using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CodeMade.GithubUpdateChecker.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        //[Ignore("Local development only")]
        [Test]
        public async Task Test_WithRealData()
        {
            var checker = VersionChecker.Create("LBognanni", "ImageViewer", new Version(0,0,0), "Image Viewer");
            await checker.NotifyIfNewVersion();
        }
    }
}
