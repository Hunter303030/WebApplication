using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication.Dto.Profile;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service.Interfase;

namespace WebApplication.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileService _profileService;

        public ProfileController(
                                ILogger<ProfileController> logger,
                                IProfileRepository profileRepository,
                                IProfileService profileService)
        {
            _logger = logger;
            _profileRepository = profileRepository;
            _profileService = profileService;
        }

        public IActionResult AuthView()
        {
            return View("~/Views/profile/Auth.cshtml");
        }

        public async Task<IActionResult> Auth(ProfileAuthDto profileAuthDto)
        {
            try
            {
                var profileSelect = await _profileRepository.Select(profileAuthDto);
                if (profileSelect != null)
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(profileSelect.Id)),
                    new Claim(ClaimTypes.Name, profileSelect.NickName),
                    new Claim(ClaimTypes.Role, profileSelect.Role.Title),
                    new Claim("ImageUrl",profileSelect.ImageUrl)
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
                    return View("~/Views/profile/Auth.cshtml", profileAuthDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при авторизации!");
                ModelState.AddModelError("ErrorAuth", "Произошла ошибка при авторизации!");
                return View("~/Views/profile/Auth.cshtml", profileAuthDto);
            }
        }

        public IActionResult RegisterView()
        {
            return View("~/Views/profile/Register.cshtml");
        }

        public async Task<IActionResult> Register(Profile profile)
        {
            try
            {
                bool profileCreated = await _profileRepository.Create(profile);
                if (profileCreated)
                {
                    return View("~/Views/profile/Auth.cshtml");
                }
                else
                {
                    ModelState.AddModelError("ErrorRegister", "Пользователь с таким псевдонимом, телефоно или почтой уже существует!");
                    return View("~/Views/profile/Register.cshtml", profile);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при регистрации!");
                ModelState.AddModelError("ErrorRegister", "Произошла ошибка при регистрации!");
                return View("~/Views/profile/Register.cshtml", profile);
            }
        }


        public async Task<IActionResult> EditView(Guid profile_id)
        {
            try
            {
                var profileEdit = await _profileRepository.GetProfile(profile_id);
                return View("~/Views/profile/Edit.cshtml", profileEdit);
            }
            catch (Exception ex)
            {
                return View("~/Views/Sharer/Error.cshtml", ex);
            }
        }
    }
}