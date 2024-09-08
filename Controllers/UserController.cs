using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using WebApplication.Dto;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult AuthView()
        {
            return View("~/Views/User/Auth.cshtml");
        }

        public async Task<IActionResult> Auth(ProfileAuthDto profileAuthDto)
        {
            try
            {                
                var userSelect = await _userRepository.Select(profileAuthDto);
                if (userSelect != null)
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userSelect.Id)),
                    new Claim(ClaimTypes.Name, userSelect.NickName),
                    new Claim(ClaimTypes.Role, userSelect.Role.Title),
                    new Claim("ImageUrl",userSelect.ImageUrl)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                    };
                    
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return RedirectToAction("List", "Course");
                }
                else
                {
                    ModelState.AddModelError("ErrorAuth", "Неверная почта или пароль!");
                    return View("~/Views/User/Auth.cshtml", profileAuthDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при авторизации!");
                ModelState.AddModelError("ErrorAuth", "Произошла ошибка при авторизации!");
                return View("~/Views/User/Auth.cshtml", profileAuthDto);
            }
        }

        public IActionResult RegisterView()
        {
            return View("~/Views/User/Register.cshtml");
        }

        public async Task<IActionResult> Register(Profile profile)
        {
            try
            {
                bool userCreated = await _userRepository.Create(profile);
                if (userCreated)
                {
                    return View("~/Views/User/Auth.cshtml");
                }
                else
                {
                    ModelState.AddModelError("ErrorRegister", "Пользователь с таким псевдонимом, телефоно или почтой уже существует!");
                    return View("~/Views/User/Register.cshtml", profile);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при регистрации!");
                ModelState.AddModelError("ErrorRegister", "Произошла ошибка при регистрации!");
                return View("~/Views/User/Register.cshtml", profile);
            }
        }

    }
}