using AutoMapper;
using WebApplication.Dto.User;
using WebApplication.Repositories.Interfaces;
using WebApplication.Service.Interfase;

namespace WebApplication.Service
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
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
