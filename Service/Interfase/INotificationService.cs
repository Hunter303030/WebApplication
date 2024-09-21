using static WebApplication.Service.NotificationService;

namespace WebApplication.Service.Interfase
{
    public interface INotificationService
    {
        public void Message(string message, MessageType messageType);
    }
}
