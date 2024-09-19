using WebApplication.Dto.Profile;

namespace WebApplication.Service.Interfase
{
    public interface IProfileCookiesService
    {
        public Task SignInAsync(ProfileCookiesDto profileCookiesDto);
        public Task SignOutAsync();
    }
}
