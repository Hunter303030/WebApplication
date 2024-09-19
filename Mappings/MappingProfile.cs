using WebApplication.Dto.Profile;
using WebApplication.Models;

namespace WebApplication.Mappings
{
    public class ProfileMapping : AutoMapper.Profile
    {
        public ProfileMapping()
        {
            CreateMap<Profile, ProfileAuthDto>();
            CreateMap<Profile, ProfileCookiesDto>();
            CreateMap<Profile, ProfileEditDto>();
            CreateMap<Profile, ProfileRegisterDto>();
            CreateMap<Profile, ProfileEditPasswordDto>();

            CreateMap<ProfileAuthDto, Profile>();
            CreateMap<ProfileCookiesDto, Profile>();
            CreateMap<ProfileEditDto, Profile>();
            CreateMap<ProfileRegisterDto, Profile>();
            CreateMap<ProfileEditPasswordDto, Profile>();
        }
    }
}
