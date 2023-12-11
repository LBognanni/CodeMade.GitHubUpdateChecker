using System.Net.Http;

namespace CodeMade.GithubUpdateChecker;

public class GitHubVersionGetter : IVersionGetter
{
    public GitHubVersionGetter(string repositoryOwner, string repositoryName, string? prefix = null, Func<Version, string>? versionFormatter = null)
    {
        RepositoryOwner = repositoryOwner;
        RepositoryName = repositoryName;
        Prefix = prefix;
        _versionFormatter = versionFormatter ?? DefaultVersionFormatter;
    }

    private static string DefaultVersionFormatter(Version version) => $"{version.Major}.{version.Minor}.{version.Build}";

    public string RepositoryOwner { get; }
    public string RepositoryName { get; }
    public string? Prefix { get; }

    private Func<Version, string> _versionFormatter;

    public async Task<Version?> GetLatestVersion()
    {
        var clientHandler = new HttpClientHandler();
        clientHandler.AllowAutoRedirect = false;

        using var client = new HttpClient(clientHandler);
        var response = await client.GetAsync($"https://github.com/{RepositoryOwner}/{RepositoryName}/releases/latest", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        var latestVersion = (response?.Headers?.Location?.AbsolutePath ?? "").Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Last();
        
        if (!string.IsNullOrEmpty(Prefix) && latestVersion.StartsWith(Prefix))
        {
            latestVersion = latestVersion.Substring(Prefix.Length);
        }

        if (Version.TryParse(latestVersion, out var v))
        {
            return v;
        }

        return null;
    }

    public string GetReleaseUrl(Version newVersion) =>
        $"https://github.com/{RepositoryOwner}/{RepositoryName}/releases/tag/{Prefix}{_versionFormatter(newVersion)}";
}
