using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto.Lesson;
using WebApplication.Service;
using WebApplication.Service.Interfase;
using WebApplication.ViewsPath;

namespace WebApplication.Controllers
{
    [Authorize]
    public class LessonController : Controller
    {
        private readonly ILogger<LessonController> _logger;
        private readonly INotificationService _notificationService;

        public LessonController(ILogger<LessonController> logger,
                                INotificationService notificationService)
        {
            _logger = logger;
            _notificationService = notificationService;
        }

        [AllowAnonymous]
        public IActionResult HandleNotification(string message, NotificationService.MessageType messageType, string viewPath, object? model = null)
        {
            _notificationService.Message(message, messageType);

            if (model != null)
            {
                return View(viewPath, model);
            }
            return View(viewPath);
        }

        public IActionResult ListLessonView(Guid courseId)
        {
            return View(ViewPaths.Lesson.ListContor);
        }

        [HttpPost]
        public IActionResult AddLesson(LessonAddDto newLesson)
        {
            return View(ViewPaths.Lesson.ListContor);
        }
    }
}
