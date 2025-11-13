using NativeNotification;
using NativeNotification.Interface;

namespace CodeMade.GithubUpdateChecker;

public class NotificationSender : INotificationSender
{
    public string AppName { get; }

    public NotificationSender(string appName)
    {
        AppName = appName;
    }

    public void Send(string title, string message, string buttonText, string url)
    {
        var manager = ManagerFactory.GetNotificationManager(new NativeNotificationOption() { AppName = this.AppName });
        var notification = manager.Create();
        notification.Title = title;
        notification.Message = message;
        notification.Buttons.Add(new ActionButton(buttonText, () => OpenUrl(url)));
        notification.Show();
    }

    private void OpenUrl(string url)
    {
        try
        {
            var psi = new System.Diagnostics.ProcessStartInfo(url)
            {
                UseShellExecute = true
            };
            System.Diagnostics.Process.Start(psi);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error opening {url}: {ex.Message}");
        }
    }
}