using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Dto.Profile;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;
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

        public async Task<Profile> Select(ProfileAuthDto profileAuthDto)
        {
            var profileSelect = await _context.Profile.Where(x => x.Email == profileAuthDto.Email).Include(x => x.Role).FirstOrDefaultAsync();

            if (profileSelect != null && _passwordService.Verify(profileAuthDto.Password, profileSelect.Password))
            {
                return profileSelect;
            }
            return null;
        }

        public async Task<bool> IsExists(ProfileRegisterDto profileRegisterDto)
        {
            return await _context.Profile.AnyAsync(x => x.NickName == profileRegisterDto.NickName || x.Email == profileRegisterDto.Email || x.Phone == profileRegisterDto.Phone);
        }

        public async Task<bool> Create(Profile profile)
        {
            if (profile == null) return false;

            await _context.Profile.AddAsync(profile);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Profile> GetProfile(Guid profile_id)
        {
            var profileEdit = await _context.Profile.Where(x => x.Id == profile_id).Include(x => x.Role).FirstOrDefaultAsync();
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

        public async Task<bool> EditPassword(Profile profile)
        {
            if (profile == null) return false;

            _context.Profile.Update(profile);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

