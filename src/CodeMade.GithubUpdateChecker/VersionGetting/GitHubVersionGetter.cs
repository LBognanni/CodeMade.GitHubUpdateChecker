namespace CodeMade.GithubUpdateChecker;

public class GitHubVersionGetter : IVersionGetter
{
    public GitHubVersionGetter(string repositoryOwner, string repositoryName)
    {
        RepositoryOwner = repositoryOwner;
        RepositoryName = repositoryName;
    }

    public string RepositoryOwner { get; }
    public string RepositoryName { get; }

    public async Task<Version?> GetLatestVersion()
    {
        var clientHandler = new HttpClientHandler();
        clientHandler.AllowAutoRedirect = false;

        using var client = new HttpClient(clientHandler);
        var response = await client.GetAsync($"https://github.com/{RepositoryOwner}/{RepositoryName}/releases/latest", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        var latestVersion = response.Headers.Location.AbsolutePath.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Last();
        if (Version.TryParse(latestVersion, out var v))
        {
            return v;
        }

        return null;
    }

    public string GetReleaseUrl(Version newVersion) =>
        $"https://github.com/{RepositoryOwner}/{RepositoryName}/releases/tag/{newVersion.Major}.{newVersion.Minor}.{newVersion.Build}";
}
