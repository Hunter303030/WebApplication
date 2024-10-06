using WebApplication.Models;
using WebApplication.Dto.Profile;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service.Interfase;

namespace WebApplication.Service
{
    public class ProfileService : IProfileService
    {
        private readonly AutoMapper.IMapper _mapper;
        private readonly IProfileRepository _profileRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IPasswordService _passwordService;
        private readonly IProfileCookiesService _profileCookiesService;

        public ProfileService(
                              AutoMapper.IMapper mapper,
                              IProfileRepository userRepository,
                              IWebHostEnvironment appEnvironment,
                              IPasswordService passwordService,
                              IProfileCookiesService profileCookiesService)
        {
            _mapper = mapper;
            _profileRepository = userRepository;
            _appEnvironment = appEnvironment;
            _passwordService = passwordService;
            _profileCookiesService = profileCookiesService;
        }

        public async Task<bool> Select(ProfileAuthDto profileAuthDto)
        {
            if (profileAuthDto == null) return false;

            var profileSelect = await _profileRepository.Select(profileAuthDto);

            if (profileSelect == null) return false;

            var profileCookies = _mapper.Map<ProfileCookiesDto>(profileSelect);

            if (profileCookies == null) return false;

            await _profileCookiesService.SignInAsync(profileCookies);

            return true;
        }

        public async Task<bool> Create(ProfileRegisterDto profileRegisterDto)
        {
            if (profileRegisterDto == null) return false;

            Random random = new Random();
            var avatars = new[] { "/Images/avatar-1.png", "/Images/avatar-2.png", "/Images/avatar-3.png" };

            if (await _profileRepository.IsExists(profileRegisterDto)) return false;

            Profile newProfile = new Profile
            {
                Id = Guid.NewGuid(),
                DateCreate = DateTime.Now,
                ImageUrl = avatars[random.Next(avatars.Length)],                
                RoleId = 3
            };

            _mapper.Map(profileRegisterDto, newProfile);

            newProfile.Password = _passwordService.Hash(profileRegisterDto.Password);

            return await _profileRepository.Create(newProfile);
        }

        public async Task<ProfileEditDto> GetProfileForEdit(Guid Id)
        {
            var userEdit = await _profileRepository.GetProfile(Id);

            if (userEdit == null) return null;

            return _mapper.Map<ProfileEditDto>(userEdit);
        }


        public async Task<Profile> GetProfile(Guid Id)
        {
            var userGet = await _profileRepository.GetProfile(Id);

            if (userGet == null) return null;

            return userGet;
        }

        public async Task<bool> Edit(ProfileEditDto profileEditDto, Guid profileId)
        {
            if (profileEditDto == null || profileId == Guid.Empty) return false;

            var userProfile = await _profileRepository.GetProfile(profileId);
            if (userProfile == null) return false;

            string newFilePath = "";
            string oldFilePath = "";

            if (profileEditDto.Avatar != null)
            {
                string folderPath = Path.Combine(_appEnvironment.WebRootPath, "Avatar", profileId.ToString());
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(profileEditDto.Avatar.FileName);
                newFilePath = Path.Combine(folderPath, fileName);

                if (!string.IsNullOrEmpty(userProfile.ImageUrl))
                {
                    oldFilePath = Path.Combine(_appEnvironment.WebRootPath, userProfile.ImageUrl.TrimStart('/'));
                }

                profileEditDto.ImageUrl = $"/Avatar/{profileId}/" + fileName;
            }

            _mapper.Map(profileEditDto, userProfile);

            bool isUpdated = await _profileRepository.Edit(userProfile);

            if (isUpdated)
            {
                if (!string.IsNullOrEmpty(newFilePath))
                {
                    using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                    {
                        await profileEditDto.Avatar.CopyToAsync(fileStream);
                    }

                    if (!string.IsNullOrEmpty(oldFilePath) && File.Exists(oldFilePath))
                    {
                        File.Delete(oldFilePath);
                    }
                }

                var profileCookies = _mapper.Map<ProfileCookiesDto>(userProfile);
                await _profileCookiesService.SignInAsync(profileCookies);

                return true;
            }
            else
            {
                if (!string.IsNullOrEmpty(newFilePath) && File.Exists(newFilePath))
                {
                    File.Delete(newFilePath);
                }

                return false;
            }
        }


        public async Task<bool> EditPassword(ProfileEditPasswordDto profileEditPasswordDto, Guid profileId)
        {
            if (profileEditPasswordDto == null || profileId == Guid.Empty)
                return false;

            var editPassword = await _profileRepository.GetProfile(profileId);

            if (editPassword == null)
                return false;

            profileEditPasswordDto.Password = _passwordService.Hash(profileEditPasswordDto.ConfirmPassword);
            var cheak = _mapper.Map(profileEditPasswordDto, editPassword);

            return await _profileRepository.EditPassword(editPassword);
        }

    }
}
