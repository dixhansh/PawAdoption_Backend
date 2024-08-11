using AutoMapper;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Repositories;

namespace PawAdoption_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepositoy userRepositoy;
        private readonly IMapper mapper;

        public UserService(IUserRepositoy userRepositoy, IMapper mapper)
        {
            this.userRepositoy = userRepositoy;
            this.mapper = mapper;
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(Guid id)
        {
            var user = await userRepositoy.FindUserById(id);
            if(user != null)
            {
                //Domain Model to dto
                return mapper.Map<UserResponseDto>(user);
            }
            return null;
        }
    }
}
