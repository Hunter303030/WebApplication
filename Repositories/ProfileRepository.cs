using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Dto.Profile;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service;

namespace WebApplication.Repositories
{
    public class ProfileRepository :IProfileRepository
    {
        private readonly DataContext _context;

        public ProfileRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Profile?> Select(ProfileAuthDto profileAuthDto)
        {            
            PasswordService service = new PasswordService();

            var userSelect = await _context.Profile.Where(x => x.Email == profileAuthDto.Email).Include(x=>x.Role).FirstOrDefaultAsync();

            if (userSelect != null && service.Verify(profileAuthDto.Password, userSelect.Password))
            {
                return userSelect;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Create(Profile profile)
        {
            Random random = new Random();
            PasswordService service = new PasswordService();
            var avatars = new[] { "/Images/avatar-1.png", "/Images/avatar-2.png", "/Images/avatar-3.png" };
            
            bool userExists = await _context.Profile.AnyAsync(x => x.NickName == profile.NickName || x.Email == profile.Email || x.Phone == profile.Phone);

            if (!userExists)
            {
                Profile newProfile = new Profile
                {
                    Id = Guid.NewGuid(),
                    NickName = profile.NickName,
                    Email = profile.Email,
                    Password = service.Hash(profile.Password),
                    Phone = profile.Phone,
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

        public async Task<Profile> GetProfile(Guid user_id)
        {
            var userEdit = await _context.Profile.Where(x => x.Id == user_id).FirstOrDefaultAsync();
            if(userEdit != null)
            {
                return userEdit;
            }
            return null;
        }
    }
}

