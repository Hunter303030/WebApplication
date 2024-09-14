using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Dto.Profile;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service;
using WebApplication.Service.Interfase;

namespace WebApplication.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DataContext _context;
        private readonly IPasswordService _passwordService;

        public ProfileRepository(DataContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public async Task<Profile?> Select(ProfileAuthDto profileAuthDto)
        {
            var profileSelect = await _context.Profile.Where(x => x.Email == profileAuthDto.Email).Include(x => x.Role).FirstOrDefaultAsync();

            if (profileSelect != null && _passwordService.Verify(profileAuthDto.Password, profileSelect.Password))
            {
                return profileSelect;
            }
            return null;
        }

        public async Task<bool> Create(Profile profile)
        {
            Random random = new Random();
            var avatars = new[] { "/Images/avatar-1.png", "/Images/avatar-2.png", "/Images/avatar-3.png" };

            bool profileExists = await _context.Profile.AnyAsync(x => x.NickName == profile.NickName || x.Email == profile.Email || x.Phone == profile.Phone);

            if (!profileExists)
            {
                Profile newProfile = new Profile
                {
                    Id = Guid.NewGuid(),
                    NickName = profile.NickName,
                    Email = profile.Email,
                    Password = _passwordService.Hash(profile.Password),
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

        public async Task<Profile> GetProfile(Guid profile_id)
        {
            var profileEdit = await _context.Profile.Where(x => x.Id == profile_id).FirstOrDefaultAsync();
            if (profileEdit != null)
            {
                return profileEdit;
            }
            return null;
        }

        public async Task<bool> Edit(Profile profile)
        {
            if (profile == null)
                return false;

            var existingProfile = await _context.Profile
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == profile.Id);

            if (existingProfile == null)
                return false;

            bool emailChanged = existingProfile.Email != profile.Email;
            bool nickNameChanged = existingProfile.NickName != profile.NickName;
            bool phoneChanged = existingProfile.Phone != profile.Phone;

            if (emailChanged || nickNameChanged || phoneChanged)
            {
                bool isDuplicate = await _context.Profile
                    .AnyAsync(x => (emailChanged && x.Email == profile.Email) ||
                                   (nickNameChanged && x.NickName == profile.NickName) ||
                                   (phoneChanged && x.Phone == profile.Phone)
                                   && x.Id != profile.Id);

                if (!isDuplicate)
                {
                    _context.Profile.Update(profile);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> EditPassword(Guid profile_Id,ProfileEditPasswordDto profileEditPasswordDto)
        {
            var profile = await _context.Profile.FirstOrDefaultAsync(x => x.Id == profile_Id);

            if(profile != null)
            {
                profile.Password = _passwordService.Hash(profileEditPasswordDto.NewPassword);
                _context.Profile.Update(profile );
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

