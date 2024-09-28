using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication.Dto.Profile;
using WebApplication.Service;
using WebApplication.Service.Interfase;
using WebApplication.ViewsPath;

namespace WebApplication.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IProfileService _profileService;
        private readonly IProfileCookiesService _profileCookiesService;
        private readonly INotificationService _notificationService;

        public ProfileController(
                                ILogger<ProfileController> logger,
                                IProfileService profileService,
                                IProfileCookiesService profileCookiesService,
                                INotificationService notificationService)
        {
            _logger = logger;
            _profileService = profileService;
            _profileCookiesService = profileCookiesService;
            _notificationService = notificationService;
        }

        public IActionResult HandleNotification(string message, NotificationService.MessageType messageType, string viewPath, object? model = null)
        {
            _notificationService.Message(message, messageType);

            if (model != null)
            {
                return View(viewPath, model);
            }
            return View(viewPath);
        }


        public IActionResult AuthView()
        {
            return View(ViewPaths.Profile.Auth);
        }

        public async Task<IActionResult> Auth(ProfileAuthDto profileAuthDto)
        {
            if (profileAuthDto == null)
            {
                _logger.LogError("Ошибка модели авторизации!");
                return HandleNotification("Ошибка модели авторизации!", NotificationService.MessageType.Error, ViewPaths.Profile.Auth, profileAuthDto);
            }
            try
            {
                bool profileSelect = await _profileService.Select(profileAuthDto);

                if (!profileSelect)
                {
                    return HandleNotification("Неверная почта или пароль!", NotificationService.MessageType.Error, ViewPaths.Profile.Auth, profileAuthDto);
                }
                _notificationService.Message("Вы успешно вошли в аккаунт!", NotificationService.MessageType.Success);
                return RedirectToAction("ListAll", "Course");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка авторизации!");
                return HandleNotification("Ошибка авторизации!", NotificationService.MessageType.Error, ViewPaths.Profile.Auth, profileAuthDto);
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _profileCookiesService.SignOutAsync();
            _notificationService.Message("Вы успешно вышли из аккаунта!", NotificationService.MessageType.Success);
            return RedirectToAction("AuthView");
        }

        public IActionResult RegisterView()
        {
            return View(ViewPaths.Profile.Register);
        }

        public async Task<IActionResult> Register(ProfileRegisterDto profileRegisterDto)
        {
            if (profileRegisterDto == null)
            {
                _logger.LogError("Ошибка модели регистрации!");
                return HandleNotification("Ошибка модели регистрации!", NotificationService.MessageType.Error, ViewPaths.Profile.Register, profileRegisterDto);
            }
            try
            {
                bool profileCreated = await _profileService.Create(profileRegisterDto);

                if (!profileCreated)
                {
                    return HandleNotification("Пользователь с такой псевданимом, почтой или телефоном уже существует!", NotificationService.MessageType.Error, ViewPaths.Profile.Register, profileRegisterDto);
                }

                _notificationService.Message("Регистриция прошла успешно!", NotificationService.MessageType.Success);
                return View(ViewPaths.Profile.Auth);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка регистрации!");
                return HandleNotification("Ошибка регистрации!", NotificationService.MessageType.Error, ViewPaths.Profile.Register, profileRegisterDto);

            }            
        }

        [Authorize]
        public async Task<IActionResult> EditView()
        {
            try
            {
                var profileId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (profileId == null)
                {
                    _logger.LogError("Ошибка уникального индификатора пользователя!");
                    return HandleNotification("Ошибка уникального индификатора пользователя!", NotificationService.MessageType.Error, ViewPaths.Profile.Edit);
                }

                var profileEdit = await _profileService.GetProfileForEdit(Guid.Parse(profileId));
                if(profileEdit == null)
                {
                    _notificationService.Message("Произошла ошибка при вызове формы редактирования!", NotificationService.MessageType.Error);
                    return RedirectToAction("List", "Course");
                }
                return View(ViewPaths.Profile.Edit, profileEdit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при вызове формы редактирования!");
                _notificationService.Message("Произошла ошибка при вызове формы редактирования!", NotificationService.MessageType.Error);
                return RedirectToAction("List", "Course");
            }
        }

        [Authorize]
        public async Task<IActionResult> Edit(ProfileEditDto profileEditDto)
        {
            if (profileEditDto == null)
            {
                _logger.LogError("Ошибка модели редактирования!");
                return HandleNotification("Ошибка модели редактирования!", NotificationService.MessageType.Error, ViewPaths.Profile.Edit, profileEditDto);
            }
            try
            {
                var profileId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (profileId == null)
                {
                    _logger.LogError("Ошибка уникального индификатора пользователя!");
                    return HandleNotification("Ошибка уникального индификатора пользователя!", NotificationService.MessageType.Error, ViewPaths.Profile.Edit);
                }

                bool cheach = await _profileService.Edit(profileEditDto, Guid.Parse(profileId));

                if (!cheach)
                {
                    return HandleNotification("Произошла ошибка редактирования!", NotificationService.MessageType.Error, ViewPaths.Profile.Edit, profileEditDto);
                }
                _notificationService.Message("Данные успешно обновлены!", NotificationService.MessageType.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка редактирования!");
                _notificationService.Message("Произошла ошибка редактирования!", NotificationService.MessageType.Error);
            }
            return View(ViewPaths.Profile.Edit, profileEditDto);
        }

        [Authorize]
        public IActionResult EditPasswordView()
        {
            return View(ViewPaths.Profile.EditPassword);
        }

        [Authorize]
        public async Task<IActionResult> EditPassword(ProfileEditPasswordDto profileEditPasswordDto)
        {
            if (profileEditPasswordDto == null)
            {
                _logger.LogError("Ошибка модели изменения пароля!");
                return HandleNotification("Ошибка модели изменения пароля!", NotificationService.MessageType.Error, ViewPaths.Profile.EditPassword, profileEditPasswordDto);
            }
            try
            {
                if (profileEditPasswordDto.Password != profileEditPasswordDto.ConfirmPassword)
                {
                    _logger.LogError("Пароль не совпадает!");
                    return HandleNotification("Пароль не совпадает!", NotificationService.MessageType.Warning, ViewPaths.Profile.EditPassword, profileEditPasswordDto);
                }

                var profileId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (profileId == null)
                {
                    _logger.LogError("Ошибка уникального индификатора пользователя!");
                    return HandleNotification("Ошибка уникального индификатора пользователя!", NotificationService.MessageType.Error, ViewPaths.Profile.EditPassword);
                }

                bool cheak = await _profileService.EditPassword(profileEditPasswordDto, Guid.Parse(profileId));

                if (!cheak)
                {
                    _logger.LogError("Произошла ошибка изменения пароля!");
                    return HandleNotification("Произошла ошибка изменения пароля!", NotificationService.MessageType.Error, ViewPaths.Profile.EditPassword, profileEditPasswordDto);
                }
                _notificationService.Message("Пароль успешно изменен!", NotificationService.MessageType.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка изменения пароля!");
                _notificationService.Message("Произошла ошибка изменения пароля!", NotificationService.MessageType.Error);
            }

            return View(ViewPaths.Profile.EditPassword, profileEditPasswordDto);
        }
    }
}