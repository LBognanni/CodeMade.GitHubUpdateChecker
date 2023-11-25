namespace CodeMade.GithubUpdateChecker;

public class VersionChecker
{
    private readonly IVersionGetter _versionGetter;
    private readonly Version _currentVersion;
    private readonly IWindowsNotification _notifier;
    private readonly ITempData _tempDataProvider;
    private readonly string _appName;
    internal const string LASTNOTIFICATIONDATE = "LastVersionNotificationTime";

    public VersionChecker(IVersionGetter versionGetter, Version currentVersion, IWindowsNotification notifier, ITempData tempDataProvider, string appName)
    {
        _versionGetter = versionGetter;
        _currentVersion = currentVersion;
        _notifier = notifier;
        _tempDataProvider = tempDataProvider;
        _appName = appName;
    }

    public TimeSpan NotificationFrequency { get; set; } = TimeSpan.FromDays(1);

    public async Task NotifyIfNewVersion()
    {
        var newVersion = await _versionGetter.GetLatestVersion().ConfigureAwait(false) ?? _currentVersion;
        if (newVersion <= _currentVersion)
            return;

        var lastNotification = _tempDataProvider.Read<DateTime>(LASTNOTIFICATIONDATE);
        if ((lastNotification + NotificationFrequency) > DateTime.Now)
            return;

        _notifier.Send($"A new version of {_appName} is available", $"You have version {_currentVersion}, click the button below to visit the latest release page", $"Download {_appName} {newVersion}", _versionGetter.GetReleaseUrl(newVersion));
        _tempDataProvider.Write(LASTNOTIFICATIONDATE, DateTime.Now);
    }

    public static VersionChecker Create(string repositoryOwner, string repositoryName, Version currentVersion, string appName)
    {
        var getter = new GitHubVersionGetter(repositoryOwner, repositoryName);
        var notifier = new WindowsNotification();
        var tempDataProvider = new FileBasedTempData();
        return new VersionChecker(getter, currentVersion, notifier, tempDataProvider, appName);
    }
}
