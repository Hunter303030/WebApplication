using Microsoft.AspNetCore.Mvc;
using WebApplication.Dto;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger,IUserRepository userRepository)
        {
            _logger = logger;
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

        public async Task<IActionResult> Register(Profile profile)
        {
            try
            {
                await _userRepository.Create(profile);
                return View("~/Views/User/Auth.cshtml");                
            }
            catch(Exception ex)
            {
                _logger.LogWarning("LogCritical {0}", ex);
                return View("~/Views/User/Register.cshtml");
            }            
        }
    }
}