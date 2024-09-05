using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult AuthView()
        {
            return View("~/Views/User/Auth.cshtml");
        }

        public IActionResult Auth(ProfileAuthDto profileAuthDto)
        {
            return View();
        }

        public IActionResult RegisterView()
        {
            return View("~/Views/User/Register.cshtml");
        }
    }
}