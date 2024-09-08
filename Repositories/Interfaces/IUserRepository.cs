using WebApplication.Dto;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<Profile?> Select(ProfileAuthDto profileAuthDto);
        public Task<bool> Create(Profile profile);
    }
}
