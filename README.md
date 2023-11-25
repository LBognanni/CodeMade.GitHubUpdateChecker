# Github Update Checker

This is a .net library and a nuget package that allows you to check for release updates on github repositories, and show a notification to the user.

It's meant to be used by applications that target Windows (at least Windows 10), and are distributed via GitHub Releases.

Usage:

```csharp
var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
var checker = VersionChecker.Create("myusername", "myrepo", currentVersion, "My App Name");
Task.Run(() => checker.NotifyIfNewVersion());
```

Some notes about defaults:
- By default, the checker will only notify the user once per day. You can change this by setting the `NotificationFrequency` property.	
- By default, the checker will use a temp file to store the last time it notified the user. You can change this by passing a custom class that implements `INotificationStorage` to the constructor of `VersionChecker`.

