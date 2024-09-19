using WebApplication.Dto.Profile;
using WebApplication.Models;

namespace WebApplication.Service.Interfase
{
    public interface IProfileService
    {
        public Task<bool> Select(ProfileAuthDto profileAuthDto);
        public Task<bool> Create(ProfileRegisterDto profileRegisterDto);
        public Task<Profile> GetProfile(Guid Id);
        public Task<ProfileEditDto> GetProfileForEdit(Guid Id);
        public Task<bool> Edit(ProfileEditDto profileEditDto, Guid profile_Id);
        public Task<bool> EditPassword(ProfileEditPasswordDto profileEditDto, Guid profile_Id);
    }
}
