using WebApplication.Dto.Profile;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        public Task<Profile> Select(ProfileAuthDto profileAuthDto);
        public Task<bool> Create(Profile profile);
        public Task<bool> IsExists(ProfileRegisterDto profileRegisterDto);
        public Task<Profile> GetProfile(Guid profile_id);
        public Task<bool> Edit(Profile profile);
        public Task<bool> EditPassword(Profile profile);
    }
}
