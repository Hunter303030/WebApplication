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
        private readonly IWebHostEnvironment _appEnvironment;

        public ProfileController(
                                ILogger<ProfileController> logger,
                                IProfileRepository profileRepository,
                                IProfileService profileService,
                                IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _profileRepository = profileRepository;
            _profileService = profileService;
            _appEnvironment = appEnvironment;
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


        public async Task<IActionResult> EditView()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var profileEdit = await _profileService.GetProfile(Guid.Parse(userId));
                return View("~/Views/profile/Edit.cshtml", profileEdit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при вызове формы редактирования!");
                return RedirectToAction("List", "Course");
            }
        }

        public async Task<IActionResult> Edit(ProfileEditDto profileEdit)
        {
            try
            {
                if (profileEdit.Avatar != null)
                {
                    string folderPath = Path.Combine(_appEnvironment.WebRootPath, "Avatar");

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(profileEdit.Avatar.FileName);
                    string filePath = Path.Combine(folderPath, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await profileEdit.Avatar.CopyToAsync(fileStream);
                    }
                    profileEdit.ImageUrl = "/Avatar/" + fileName;
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                bool cheach = await _profileService.Edit(profileEdit, Guid.Parse(userId));

                if (cheach)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userId)),
                        new Claim(ClaimTypes.Name, profileEdit.NickName),
                        new Claim("ImageUrl",profileEdit.ImageUrl)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                    };

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                }
                else
                {
                    ModelState.AddModelError("ErrorEdit", "Данные уже заняты!");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrorEdit", "Произошла ошибка при редактировании!");
                _logger.LogError(ex, "Произошла ошибка при редактирования!");
            }
            return View("~/Views/profile/Edit.cshtml", profileEdit);
        }

        public IActionResult EditPasswordView()
        {
            return View("~/Views/Profile/EditPassword.cshtml");
        }

        public async Task<IActionResult> EditPassword(ProfileEditPasswordDto profileEditPasswordDto)
        {
            try
            {
                if(profileEditPasswordDto != null)
                {
                    var profile_Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    bool cheak = await _profileService.EditPassword(profileEditPasswordDto, Guid.Parse(profile_Id));

                    if (cheak)
                    {
                        return await EditView();
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorEditPassword", "Произошла ошибка во время изменения пароля!");
                    }
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("ErrorEditPassword", "Произошла ошибка во время изменения пароля!");
                _logger.LogError(ex, "Произошла ошибка во время изменения пароля!");
            }
            return View("~/Views/Profile/EditPassword.cshtml");
        }
    }
}