using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebApplication.Service.Interfase;

namespace WebApplication.Service
{
    public class NotificationService: INotificationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public NotificationService(IHttpContextAccessor httpContextAccessor,
                                   ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        public enum MessageType
        {
            Success,
            Error,
            Warning,
            Info
        }

        public void Message(string message, MessageType messageType)
        {
            var tempData = _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);

            if (tempData != null)
            {
                tempData["Message"] = message;
                tempData["MessageType"] = messageType.ToString();
            }
        }
    }
}
