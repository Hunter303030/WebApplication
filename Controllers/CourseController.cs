using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication.Dto.Course;
using WebApplication.Service;
using WebApplication.Service.Interfase;
using WebApplication.ViewsPath;

namespace WebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;
        private readonly ICourseService _courseService;
        private readonly INotificationService _notificationService;

        public CourseController(
                                ILogger<CourseController> logger,
                                ICourseService courseService,
                                INotificationService notificationService)
        {
            _logger = logger;
            _courseService = courseService;
            _notificationService = notificationService;
        }

        public IActionResult HandleNotification(string message, NotificationService.MessageType messageType, string viewPath, object? model = null)
        {
            _notificationService.Message(message, messageType);

            if (model != null)
            {
                return View(viewPath, model);
            }
            return View(viewPath);
        }

        public IActionResult ListAll()
        {
            return View(ViewPaths.Course.ListAll);
        }



        public IActionResult ListControl()
        {
            return View(ViewPaths.Course.ListControl);
        }

        [Authorize]
        public IActionResult AddCourseView()
        {
            return View(ViewPaths.Course.Add);
        }

        [Authorize]
        public async Task<IActionResult> AddCourse(CourseAddDto courseAddDto)
        {
            try
            {
                if (courseAddDto == null)
                {
                    _logger.LogError("Ошибка модели курса!");
                    return HandleNotification("Ошибка модели курса!", NotificationService.MessageType.Error, ViewPaths.Course.Add);
                }

                var profileId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (profileId == null)
                {
                    _logger.LogError("Ошибка уникального индификатора пользователя!");
                    return HandleNotification("Ошибка уникального индификатора пользователя!", NotificationService.MessageType.Error, ViewPaths.Course.Add);
                }

                bool cheack = await _courseService.Add(courseAddDto, Guid.Parse(profileId));

                if (cheack)
                {
                    _notificationService.Message("Курс успешно создан!", NotificationService.MessageType.Success);
                    return RedirectToAction("ListAll");
                }
                else
                {
                    _logger.LogError("Произошла ошибка во время добавления курса!");
                    return HandleNotification("Произошла ошибка во время добавления курса!", NotificationService.MessageType.Error, ViewPaths.Course.Add);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка во время добавления курса!");
                _notificationService.Message("Произошла ошибка во время добавления курса!", NotificationService.MessageType.Error);
            }
            return View(ViewPaths.Course.Add);
        }
    }
}
