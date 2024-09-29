using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication.Dto.Course;
using WebApplication.Service;
using WebApplication.Service.Interfase;
using WebApplication.ViewsPath;

namespace WebApplication.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        public async Task<IActionResult> ListAllView()
        {
            try
            {
                var list = await _courseService.ListAll();

                if (list == null)
                {
                    _logger.LogInformation("������ ������ ����!");
                    _notificationService.Message("������ ������ ����!", NotificationService.MessageType.Error);
                }
                return View(ViewPaths.Course.ListAll/*, list*/);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ��� ��������� ������ ������!");
                _notificationService.Message("������ ��� ��������� ������ ������!", NotificationService.MessageType.Error);
                return View(ViewPaths.Course.ListAll);
            }
        }

        public async Task<IActionResult> ListControlView()
        {
            var profileId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (profileId == null)
            {
                return HandleNotification("������ ����������� ������������ ������������!", NotificationService.MessageType.Error, ViewPaths.Course.ListAll);
            }

            try
            {
                var list = await _courseService.ListControl(Guid.Parse(profileId));

                if (list == null)
                {
                    _logger.LogInformation("������ ������ ����!");
                    _notificationService.Message("������ ������ ����!", NotificationService.MessageType.Error);
                }
                return View(ViewPaths.Course.ListControl,list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ��� ��������� ������ ������!");
                _notificationService.Message("������ ��� ��������� ������ ������!", NotificationService.MessageType.Error);
                return View(ViewPaths.Course.ListAll);
            }
        }

        public IActionResult AddCourseView()
        {
            return View(ViewPaths.Course.Add);
        }

        public async Task<IActionResult> AddCourse(CourseAddDto courseAddDto)
        {
            try
            {
                if (courseAddDto == null)
                {
                    _logger.LogError("������ ������ �����!");
                    return HandleNotification("������ ������ �����!", NotificationService.MessageType.Error, ViewPaths.Course.Add);
                }

                var profileId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (profileId == null)
                {
                    _logger.LogError("������ ����������� ������������ ������������!");
                    return HandleNotification("������ ����������� ������������ ������������!", NotificationService.MessageType.Error, ViewPaths.Course.Add);
                }

                bool cheack = await _courseService.Add(courseAddDto, Guid.Parse(profileId));

                if (cheack)
                {
                    _notificationService.Message("���� ������� ������!", NotificationService.MessageType.Success);
                    return RedirectToAction("ListControlView");
                }
                else
                {
                    _logger.LogError("��������� ������ �� ����� ���������� �����!");
                    return HandleNotification("��������� ������ �� ����� ���������� �����!", NotificationService.MessageType.Error, ViewPaths.Course.Add);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "��������� ������ �� ����� ���������� �����!");
                _notificationService.Message("��������� ������ �� ����� ���������� �����!", NotificationService.MessageType.Error);
            }
            return View(ViewPaths.Course.Add);
        }

        public async Task<IActionResult> EditCourseView(Guid courseId)
        {
            try
            {
                if(courseId == Guid.Empty)
                {
                    _logger.LogError("������ ����������� ������������ �����!");
                    _notificationService.Message("������ ����������� ������������ �����!", NotificationService.MessageType.Error);
                    return RedirectToAction("ListControlView", "Course");
                }
                
                var edit = await _courseService.GetCourse(courseId);

                if(edit == null)
                {
                    _logger.LogError("������ ��������� ������ �����!");
                    _notificationService.Message("������ ��������� ������ �����!", NotificationService.MessageType.Error);
                    return RedirectToAction("ListControlView", "Course");
                }

                return View(ViewPaths.Course.Edit, edit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"������ ��������� ������ �����!");
                _notificationService.Message("������ ��������� ������ �����!", NotificationService.MessageType.Error);
                return RedirectToAction("ListControlView", "Course");
            }
        }

        public async Task<IActionResult> EditCourse(CourseEditDto courseEditDto)
        {
            if(courseEditDto == null)
            {
                _logger.LogError("������ ������ �������������� �����!");
                return HandleNotification("������ ������ �������������� �����!", NotificationService.MessageType.Error, ViewPaths.Course.Edit, courseEditDto);
            }
            try
            {
                bool cheak = await _courseService.Edit(courseEditDto);

                if(cheak)
                {
                    _notificationService.Message("��������� ���������!", NotificationService.MessageType.Success);
                }
                else
                {
                    _notificationService.Message("�������� ������ ��� ����������!", NotificationService.MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("������ �������������� �����!");
                _notificationService.Message("������ �������������� �����!", NotificationService.MessageType.Error);
            }
            return View(ViewPaths.Course.Edit, courseEditDto);
        }

        public async Task<IActionResult> DeleteCourse(Guid courseId)
        {
            if(courseId == Guid.Empty)
            {
                _logger.LogError("������ ����������� ������������ �����!");
                _notificationService.Message("������ ����������� ������������ �����!", NotificationService.MessageType.Error);
                return RedirectToAction("ListControlView", "Course");
            }

            try
            {
                bool isDelete = await _courseService.Delete(courseId);

                if(isDelete)
                {
                    _notificationService.Message("���� ������� ������!", NotificationService.MessageType.Success);
                }
                else
                {
                    _notificationService.Message("��������� ������ ��� �������� �����!", NotificationService.MessageType.Error);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("������ �������� �����!");
                _notificationService.Message("������ �������� �����!", NotificationService.MessageType.Error);
            }
            return RedirectToAction("ListControlView", "Course");
        }
    }
}
