using AutoMapper;
using Azure.Core;
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

        public async Task<Models.Profile> Select(ProfileAuthDto profileAuthDto)
        {
            if (profileAuthDto == null) return null; 
            
            return await _profileRepository.Select(profileAuthDto);
        }

        public async Task<bool> Create(Models.Profile profile)
        {
            if (profile == null) return false;

            var result = await _profileRepository.Create(profile);
            return result != null;
        }


        public async Task<ProfileEditDto> GetProfile(Guid Id)
        {
            var userEdit = await _profileRepository.GetProfile(Id);

            if (userEdit == null) return null;
            
            return _mapper.Map<ProfileEditDto>(userEdit);            
        }

        public async Task<bool> Edit(ProfileEditDto profileEditDto, Guid profileId)
        {
            var userProfile = await _profileRepository.GetProfile(profileId);

            if (userProfile == null) return false;

            _mapper.Map(profileEditDto, userProfile);
            return await _profileRepository.Edit(userProfile);
        }


        public async Task<bool> EditPassword(ProfileEditPasswordDto profileEditDto, Guid profile_Id)
        {
            if (profileEditDto == null && profile_Id == Guid.Empty) return false;

            return await _profileRepository.EditPassword(profile_Id, profileEditDto);            
        }
    }
}
