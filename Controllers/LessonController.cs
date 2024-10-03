using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto.Lesson;
using WebApplication.Models;
using WebApplication.ViewsPath;

namespace WebApplication.Controllers
{
    [Authorize]
    public class LessonController : Controller
    {
        public IActionResult ListLessonView(Guid courseId)
        {
            return View(ViewPaths.Lesson.ListContor);
        }

        [HttpPost]
        public IActionResult AddLesson([FromBody] LessonAddDto newLesson)
        {
            return View(ViewPaths.Lesson.ListContor);
        }
    }
}
