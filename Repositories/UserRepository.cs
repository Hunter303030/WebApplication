using WebApplication.Data;
using WebApplication.Dto;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class UserRepository :IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void Select(ProfileAuthDto profileAuthDto)
        {

        }
    }
}
