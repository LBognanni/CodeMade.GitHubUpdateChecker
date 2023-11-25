using Microsoft.Toolkit.Uwp.Notifications;

namespace CodeMade.GithubUpdateChecker;

public class WindowsNotification : IWindowsNotification
{
    public void Send(string title, string message, string buttonText, string url)
    {
        new ToastContentBuilder()
            .AddText(title)
            .AddText(message)
            .AddButton(buttonText, ToastActivationType.Protocol, url)
            .Show();
    }
}