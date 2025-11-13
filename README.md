# GitHub Update Checker

[![NuGet](https://img.shields.io/nuget/v/CodeMade.GitHubUpdateChecker.svg)](https://www.nuget.org/packages/CodeMade.GitHubUpdateChecker/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A .NET library that checks for release updates on GitHub repositories and displays native desktop notifications to users.

Requires .NET 8 or later.

## Features

- ✅ Cross-platform support (Windows 10+, macOS, Linux)
- ✅ Native desktop notifications
- ✅ Configurable notification frequency
- ✅ Customizable version tag formats
- ✅ Minimal dependencies


## Installation

Install via NuGet Package Manager:

```bash
dotnet add package CodeMade.GitHubUpdateChecker
```

Or via Package Manager Console:

```powershell
Install-Package CodeMade.GitHubUpdateChecker
```


## Quick Start

```csharp
var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
var checker = VersionChecker.Create("myusername", "myrepo", currentVersion, "My App Name");
Task.Run(() => checker.NotifyIfNewVersion());
```



## Configuration

### Custom Notification Frequency

```csharp
var checker = VersionChecker.Create(
  "myusername", 
  "myrepo", 
  currentVersion, 
  "My App Name");
checker.NotificationFrequency = TimeSpan.FromHours(12); // Notify every 12 hours
Task.Run(() => checker.NotifyIfNewVersion());
```

### Version Tag Prefix

If your releases use tags like `v1.0.0`:

```csharp
var checker = VersionChecker.Create(
    "myusername", 
    "myrepo", 
    currentVersion, 
    "My App Name",
    versionPrefix: "v"
);
```

### Custom Version Formatter

If your version tags have a custom format:

```csharp
var checker = VersionChecker.Create(
    "myusername", 
    "myrepo", 
    currentVersion, 
    "My App Name",
    versionPrefix: null,
    versionFormatter: version => $"Release-{version.Major}.{version.Minor}"
);
```

### Custom Storage

If you need to customize where the last notification time is stored, you can implement `ITempData` and create the `VersionChecker` manually:

```csharp
using CodeMade.GithubUpdateChecker;

public class MyCustomStorage : ITempData
{
    public T? Read<T>(string key)
    {
        // Your custom read implementation
        // e.g., read from database, app settings, etc.
    }

    public void Write<T>(string key, T value)
    {
        // Your custom write implementation
    }
}

// Create VersionChecker with custom storage
var versionGetter = new GitHubVersionGetter("myusername", "myrepo");
var notifier = new NotificationSender("My App Name");
var customStorage = new MyCustomStorage();

var checker = new VersionChecker(
    versionGetter,
    currentVersion,
    notifier,
    customStorage,
    "My App Name"
);

await checker.NotifyIfNewVersion();
```


## Default Behavior

- **Notification frequency**: Once per day
- **Storage**: Temporary file in system temp directory
- **Version format**: Standard semantic versioning (e.g., `1.0.0`)
- **Tag prefix**: None

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

[MIT License](LICENSE) 

### Credits
Cross platform desktop notifications are implemented using the [NativeNotification](https://github.com/Jeric-X/NativeNotification) library

