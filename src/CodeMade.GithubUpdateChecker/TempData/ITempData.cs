namespace CodeMade.GithubUpdateChecker;

public interface ITempData
{
    void Write<T>(string what, T value);
    T? Read<T>(string what);
}
