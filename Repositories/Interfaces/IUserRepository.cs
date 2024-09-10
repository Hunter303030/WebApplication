using WebApplication.Dto.User;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<Profile?> Select(ProfileAuthDto profileAuthDto);
        public Task<bool> Create(Profile profile);
        public Task<Profile> GetProfile(Guid user_id);
    }
}
