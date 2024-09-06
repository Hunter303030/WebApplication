using WebApplication.Dto;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public void Select(ProfileAuthDto profileAuthDto);
        public void Add(Profile profile);
    }
}
