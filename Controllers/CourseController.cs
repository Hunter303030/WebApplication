using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;

        public CourseController(ILogger<CourseController> logger)
        {
            _logger = logger;
        }

        public IActionResult CourseListView()
        {
            return View("~/Views/Course/List.cshtml");
        }              
    }
}
