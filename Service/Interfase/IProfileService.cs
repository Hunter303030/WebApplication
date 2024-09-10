using WebApplication.Dto.Profile;

namespace WebApplication.Service.Interfase
{
    public interface IProfileService
    {
        public Task<ProfileEditDto> GetProfile(Guid Id);
    }
}
