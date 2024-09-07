using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Dto;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service;

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

        public async Task<bool> Create(Profile profile)
        {
            Random random = new Random();
            PasswordService service = new PasswordService();
            var avatars = new[] { "/images/avatar-1.png", "/images/avatar-2.png", "/images/avatar-3.png" };
            
            bool userExists = await _context.Profile.AnyAsync(x => x.NickName == profile.NickName || x.Email == profile.Email);

            if (!userExists)
            {
                Profile newProfile = new Profile
                {
                    Id = Guid.NewGuid(),
                    NickName = profile.NickName,
                    Email = profile.Email,
                    Password = service.Hash(profile.Password),
                    DateCreate = DateTime.Now,
                    ImageUrl = avatars[random.Next(avatars.Length)],
                    Role_Id = 3
                };
                
                await _context.Profile.AddAsync(newProfile);
                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

    }
}

