using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication.Dto.Course;
using WebApplication.Service.Interfase;

namespace WebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;
        private readonly ICourseService _courseService;

        public CourseController(
                                ILogger<CourseController> logger,
                                ICourseService courseService)
        {
            _logger = logger;
            _courseService = courseService;
        }

        public IActionResult List()
        {
            return View("~/Views/Course/List.cshtml");
        }

        public IActionResult AddCourseView()
        {
            return View("~/Views/Course/Add.cshtml");
        }

        public async Task<IActionResult> AddCourse(CourseAddDto courseAddDto)
        {
            try
            {
                if (courseAddDto != null)
                {
                    var profileId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    if (profileId == null)
                    {
                        ModelState.AddModelError("ErrorAddCourse", "������ ����������� ������������ ������������!");
                        return View("~/Views/Course/Add.cshtml");
                    }

                    bool cheack = await _courseService.Add(courseAddDto, Guid.Parse(profileId));

                    if (cheack)
                    {
                        return List();
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorAddCourse", "��������� ������ �� ����� ���������� �����");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "��������� ������ �� ����� ���������� �����!");
                ModelState.AddModelError("ErrorAddCourse", "��������� ������ �� ����� ���������� �����");
            }
            return View("~/Views/Course/Add.cshtml");
        }
    }
}
