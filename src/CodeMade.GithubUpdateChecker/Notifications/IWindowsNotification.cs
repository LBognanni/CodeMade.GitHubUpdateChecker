namespace CodeMade.GithubUpdateChecker;

public interface IWindowsNotification
{
    void Send(string title, string message, string buttonText, string url);
}
