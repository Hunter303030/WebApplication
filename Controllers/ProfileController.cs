using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication.Dto.Profile;
using WebApplication.Service.Interfase;

namespace WebApplication.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IProfileService _profileService;
        private readonly IProfileCookiesService _profileCookiesService;

        public ProfileController(
                                ILogger<ProfileController> logger,
                                IProfileService profileService,
                                IProfileCookiesService profileCookiesService)
        {
            _logger = logger;
            _profileService = profileService;
            _profileCookiesService = profileCookiesService;
        }

        public IActionResult AuthView()
        {
            return View("~/Views/profile/Auth.cshtml");
        }

        public async Task<IActionResult> Auth(ProfileAuthDto profileAuthDto)
        {
            if (profileAuthDto == null)
            {
                _logger.LogError("Ошибка модели авторизации!");
                ModelState.AddModelError("ErrorAuth", "Ошибка модели авторизации!");
                return View("~/Views/Profile/Auth.cshtml", profileAuthDto);
            }
            try
            {
                bool profileSelect = await _profileService.Select(profileAuthDto);

                if (!profileSelect)
                {
                    ModelState.AddModelError("ErrorAuth", "Неверная почта или пароль!");
                    return View("~/Views/Profile/Auth.cshtml", profileAuthDto);
                }

                return RedirectToAction("List", "Course");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка авторизации!");
                ModelState.AddModelError("ErrorAuth", "Ошибка авторизации!");
                return View("~/Views/Profile/Auth.cshtml", profileAuthDto);
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _profileCookiesService.SignOutAsync();
            return View("~/Views/Profile/Auth.cshtml");
        }

        public IActionResult RegisterView()
        {
            return View("~/Views/Profile/Register.cshtml");
        }

        public async Task<IActionResult> Register(ProfileRegisterDto profileRegisterDto)
        {
            if (profileRegisterDto== null)
            {
                _logger.LogError("Ошибка модели регистрации!");
                ModelState.AddModelError("ErrorRegister", "Ошибка модели регистрации!");
                return View("~/Views/Profile/Register.cshtml", profileRegisterDto);
            }
            try
            {
                bool profileCreated = await _profileService.Create(profileRegisterDto);

                if (profileCreated)
                {
                    return View("~/Views/Profile/Auth.cshtml");
                }
                else
                {
                    ModelState.AddModelError("ErrorRegister", "Пользователь с таким псевдонимом, телефоно или почтой уже существует!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка регистрации!");
                ModelState.AddModelError("ErrorRegister", "Произошла ошибка регистрации!");
            }
            return View("~/Views/Profile/Register.cshtml", profileRegisterDto);
        }

        [Authorize]
        public async Task<IActionResult> EditView()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var profileEdit = await _profileService.GetProfileForEdit(Guid.Parse(userId));
                return View("~/Views/Profile/Edit.cshtml", profileEdit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при вызове формы редактирования!");
                return RedirectToAction("List", "Course");
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(ProfileEditDto profileEditDto)
        {
            if (profileEditDto == null)
            {
                _logger.LogError("Ошибка модели редактирования!");
                ModelState.AddModelError("ErrorEdit", "Ошибка модели редактирования!");
                return View("~/Views/Profile/Edit.cshtml", profileEditDto);
            }
            try
            {
                var profileId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                bool cheach = await _profileService.Edit(profileEditDto, Guid.Parse(profileId));

                if (!cheach)
                {
                    ModelState.AddModelError("ErrorEdit", "Данные уже заняты!");
                }
                return View("~/Views/Profile/Edit.cshtml", profileEditDto);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrorEdit", "Произошла ошибка редактировании!");
                _logger.LogError(ex, "Произошла ошибка редактирования!");
            }
            return View("~/Views/Profile/Edit.cshtml", profileEditDto);
        }

        [Authorize]
        public IActionResult EditPasswordView()
        {
            return View("~/Views/Profile/EditPassword.cshtml");
        }

        [Authorize]
        public async Task<IActionResult> EditPassword(ProfileEditPasswordDto profileEditPasswordDto)
        {
            if (profileEditPasswordDto == null)
            {
                _logger.LogError("Ошибка модели изменения пароля!");
                ModelState.AddModelError("ErrorEditPassword", "Ошибка модели изменения пароля!");
            }
            try
            {
                if(profileEditPasswordDto.NewPassword != profileEditPasswordDto.ConfirmPassword)
                {
                    _logger.LogError("Пароль не совпадает!");
                    ModelState.AddModelError("ErrorEditPassword", "Пароль не совпадает!");
                }

                var profile_Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                bool cheak = await _profileService.EditPassword(profileEditPasswordDto, Guid.Parse(profile_Id));

                if (!cheak)
                {
                    ModelState.AddModelError("ErrorEditPassword", "Произошла ошибка изменения пароля!");
                    _logger.LogError("Произошла ошибка изменения пароля!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ErrorEditPassword", "Произошла ошибка изменения пароля!");
                _logger.LogError(ex, "Произошла ошибка изменения пароля!");
            }

            return View("~/Views/Profile/EditPassword.cshtml", profileEditPasswordDto);
        }
    }
}