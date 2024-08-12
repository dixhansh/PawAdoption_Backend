using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Repositories;

namespace PawAdoption_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepositoy userRepositoy;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public UserService(IUserRepositoy userRepositoy, IMapper mapper, UserManager<User> userManager)
        {
            this.userRepositoy = userRepositoy;
            this.mapper = mapper;
            this.userManager = userManager;
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

        public async Task<UserResponseDto?> UpdateUserAsync(Guid id, UserRequestDto userDto)
        {
            var userUpdates = mapper.Map<User>(userDto);
            var updatedUser = await userRepositoy.RenewUserAsync(id, userUpdates);
            if(updatedUser != null)
            {
                //convert domain model to dto
                return(mapper.Map<UserResponseDto>(updatedUser));
            }
            return null;
        }


        public async Task<UserResponseDto?> DeleteUserAsync(Guid id)
        {
            var userToDelete = await userRepositoy.FindUserById(id);
            if(userToDelete != null)
            {
                var deletedUser = await userRepositoy.RemoveUserAsync(userToDelete);
                
                //Domain model to dto
                return(mapper.Map<UserResponseDto>(deletedUser));
            }
            return null;
        }

        public async Task<IdentityResult> UpdateOriginalPasswordAsync(Guid userId, PasswordRequestDto passwordDto)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new UnauthorizedAccessException("User not found");
            }

            var changePasswordResult = await userManager.ChangePasswordAsync(user, passwordDto.OldPassword, passwordDto.NewPassword);

            return changePasswordResult;

        }

        public async Task<List<UserResponseDto>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            var userList = await userRepositoy.GetAllFromDbAsync(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);

            return (mapper.Map<List<UserResponseDto>>(userList));

        }

        public async Task<List<ImageResponseDto>?> GetAllUserImagesByIdAsync(Guid id)
        {
            var existingUser = await userRepositoy.FindUserById(id);
            if(existingUser != null)
            {
                var ImageList = await userRepositoy.FindAllUserImagesByIdAsync(existingUser);
                //model to dto
                return(mapper.Map<List<ImageResponseDto>>(ImageList));
            }
            return null;
        }
    }
}
