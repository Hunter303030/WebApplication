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
        private readonly ILessonService _lessonService;

        public LessonController(ILogger<LessonController> logger,
                                INotificationService notificationService,
                                ILessonService lessonService)
        {
            _logger = logger;
            _notificationService = notificationService;
            _lessonService = lessonService;
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

        public async Task<IActionResult> ListLessonView(Guid courseId)
        {
            if (courseId == Guid.Empty)
            {
                _logger.LogError("Ошибка уникального индификатора курса!");
                _notificationService.Message("Ошибка уникального индификатора курса!", NotificationService.MessageType.Error);
                return RedirectToAction("ListControlView", "Course");
            }

            try
            {
                var list = await _lessonService.List(courseId);
                return View(ViewPaths.Lesson.ListContor, list);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Ошибка вывода списка уроков!");
                _notificationService.Message("Ошибка вывода списка уроков!", NotificationService.MessageType.Error);
                return RedirectToAction("ListControlView", "Course");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddLesson(LessonAddDto lessonAddDto)
        {
            if(lessonAddDto == null)
            {
                _logger.LogError("Ошибка модели урока!");
                _notificationService.Message("Ошибка модели урока!", NotificationService.MessageType.Error);
            }

            try
            {
                bool isAdd = await _lessonService.Add(lessonAddDto);

                if(isAdd)
                {
                    _notificationService.Message("Урок успешно добавлен!", NotificationService.MessageType.Success);
                    return RedirectToAction("ListLessonView", "Lesson", new { courseId = lessonAddDto.CourseId });
                }

                _logger.LogError("Ошибка добавления урока!");
                _notificationService.Message("Ошибка добавления урока!", NotificationService.MessageType.Error);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Ошибка добавления урока!");
                _notificationService.Message("Ошибка добавления урока!", NotificationService.MessageType.Error);
            }
            return RedirectToAction("ListLessonView", "Lesson", new { courseId = lessonAddDto.CourseId });
        }
    }
}
