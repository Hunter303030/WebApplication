using AutoMapper;
using WebApplication.Dto.Profile;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service.Interfase;

namespace WebApplication.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IMapper mapper, IProfileRepository userRepository)
        {
            _mapper = mapper;
            _profileRepository = userRepository;
        }

        public async Task<ProfileEditDto> GetProfile(Guid Id)
        {
            var userEdit = await _profileRepository.GetProfile(Id);

            if (userEdit != null)
            {
                return _mapper.Map<ProfileEditDto>(userEdit);
            }
            return null;
        }

        public async Task<bool> Edit(ProfileEditDto profileEditDto, Guid profile_Id)
        {
            var userSelect = await _profileRepository.GetProfile(profile_Id);

            if (userSelect != null)
            {
                _mapper.Map(profileEditDto, userSelect);
                bool cheack = await _profileRepository.Edit(userSelect);
                if (cheack)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> EditPassword(ProfileEditPasswordDto profileEditDto, Guid profile_Id)
        {
            if(profileEditDto != null && profile_Id != Guid.Empty)
            {
                bool cheak = await _profileRepository.EditPassword(profile_Id, profileEditDto);

                if (cheak)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
