namespace CodeMade.GithubUpdateChecker;

public interface INotificationSender
{
    void Send(string title, string message, string buttonText, string url);
}
