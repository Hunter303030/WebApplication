using WebApplication.Dto.User;

namespace WebApplication.Service.Interfase
{
    public interface IUserService
    {
        public Task<ProfileEditDto> GetProfile(Guid Id);
    }
}
