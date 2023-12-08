using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodeMade.GithubUpdateChecker.Tests;

public class FileBasedTempDataTests
{
    public static IEnumerable<object> RandomData()
    {
        yield return "a string";
        yield return 1;
        yield return (decimal)123.938493;
        yield return DateTime.Today;
        yield return DateTime.Now;
        yield return DateTimeOffset.Now;
        yield return new
        {
            SomeProperty = "SomeValue",
            SomeIntProperty = 999
        };
    }

    [TestCaseSource(nameof(RandomData))]
    public void AfterSavingTempData_CanReadItBack<T>(T data)
    {
        var sut = new FileBasedTempData();
        sut.EnsureEmpty();
        sut.Write("test", data);
        var readData = sut.Read<T>("test");
        Assert.That(readData, Is.EqualTo(data));
    }

    [Test]
    public void WhenNoData_DefaultIsReturned()
    {
        var sut = new FileBasedTempData();
        sut.EnsureEmpty();
        var n = sut.Read<int>("aa");
        Assert.That(n, Is.EqualTo(0));
        
        var s = sut.Read<string>("aab");
        Assert.That(s, Is.Null);

        var dt = sut.Read<DateTime>("aac");
        Assert.That(dt, Is.EqualTo(default(DateTime)));
    }

    [Test]
    public void WhenSavingManyProperties_AllCanBeReadBack()
    {
        var sut = new FileBasedTempData();
        sut.EnsureEmpty();
        sut.Write("a string", "hello");
        sut.Write("an int", 1);
        sut.Write("an object", new { Name = "Abc123", Age = 99 });
        var s = sut.Read<string>("a string");
        Assert.That(s, Is.EqualTo("hello"));
        var i = sut.Read<int>("an int");
        Assert.That(i, Is.EqualTo(1));
    }
}