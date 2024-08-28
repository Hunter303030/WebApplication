using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        public IActionResult AuthView()
        {
            return View("~/Views/User/Auth.cshtml");
        }
    }
}
