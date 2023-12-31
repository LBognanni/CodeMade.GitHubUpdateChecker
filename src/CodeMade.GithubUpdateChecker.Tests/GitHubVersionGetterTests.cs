﻿using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CodeMade.GithubUpdateChecker.Tests;

public class GitHubVersionGetterTests
{
    [Test]
    public void GetReleaseUrl_ReturnsTheCorrectUrl()
    {
        var getter = new GitHubVersionGetter("LBognanni", "ImageViewer");
        var url = getter.GetReleaseUrl(new Version(1, 0, 0));
        Assert.That(url, Is.EqualTo("https://github.com/LBognanni/ImageViewer/releases/tag/1.0.0"));
    }

    [Test]
    public void GetReleaseUrl_with_v_ReturnsTheCorrectUrl()
    {
        var getter = new GitHubVersionGetter("LBognanni", "ImageViewer", "v");
        var url = getter.GetReleaseUrl(new Version(1, 0, 0));
        Assert.That(url, Is.EqualTo("https://github.com/LBognanni/ImageViewer/releases/tag/v1.0.0"));
    }

    [Test]
    public void GetReleaseUrl_with_custom_formatter_ReturnsTheCorrectUrl()
    {
        var getter = new GitHubVersionGetter("LBognanni", "ImageViewer", "v", v=>v.Major.ToString());
        var url = getter.GetReleaseUrl(new Version(1, 0, 0));
        Assert.That(url, Is.EqualTo("https://github.com/LBognanni/ImageViewer/releases/tag/v1"));
    }

    [Ignore("development only")]
    [Test]
    public async Task GetLatestVersion_Works()
    {
        var version = await (new GitHubVersionGetter("LBognanni", "ImageViewer")).GetLatestVersion();
        Assert.IsNotNull(version);
        Assert.That(version, Is.EqualTo(new Version(1,11,1)));
    }
}