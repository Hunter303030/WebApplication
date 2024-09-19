using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WebApplication.Dto.Profile;
using WebApplication.Service.Interfase;

namespace WebApplication.Service
{
    public class ProfileCookiesService : IProfileCookiesService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileCookiesService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SignInAsync(ProfileCookiesDto profileCookiesDto)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, profileCookiesDto.Id.ToString()),
                    new Claim(ClaimTypes.Name, profileCookiesDto.NickName),
                    new Claim(ClaimTypes.Role, profileCookiesDto.Role.Title),
                    new Claim("ImageUrl", profileCookiesDto.ImageUrl)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                };

                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
            }
            catch { }            
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
