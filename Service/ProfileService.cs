using AutoMapper;
using WebApplication.Dto.Profile;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service.Interfase;

namespace WebApplication.Service
{
    public class ProfileService: IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IProfileRepository _userRepository;

        public ProfileService(IMapper mapper, IProfileRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ProfileEditDto> GetProfile(Guid Id)
        {
            var userEdit = await _userRepository.GetProfile(Id);

            if (userEdit != null)
            {  
                return _mapper.Map<ProfileEditDto>(userEdit);
            }
            return null;
        }
    }
}
