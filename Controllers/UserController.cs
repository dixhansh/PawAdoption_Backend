using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawAdoption_Backend.CustomActionFilters;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Services;

namespace PawAdoption_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        //get user by id 
        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var userResponseDto = await userService.GetUserByIdAsync(id);
            if (userResponseDto != null)
            {
                return Ok(userResponseDto);
            }
            return BadRequest("User not found !!!");

        }

        //update user details
        [HttpPut]
        [Route("Update/{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] UserRequestDto userDto)
        {
            var updatedUserDto = await userService.UpdateUserAsync(id, userDto);
            if (updatedUserDto != null)
            {
                return Ok(updatedUserDto);
            }
            return BadRequest("Updated are not applied !");
        }

        [HttpDelete]
        [Route("Delete/{id:Guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var deletedUserDto = await userService.DeleteUserAsync(id);
            if(deletedUserDto != null)
            {
                return Ok(deletedUserDto);
            }
            return BadRequest("User not deleted !");
        }

        //Update password if Original password is known
        [HttpPut]
        [Route("UpdateOriginalPassword/{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateOriginalPassword([FromRoute] Guid id, [FromBody] PasswordRequestDto passwordRequestDto)
        {
            var result = await userService.UpdateOriginalPasswordAsync(id, passwordRequestDto); 
          
            if (result.Succeeded)
            {
                return Ok("Password changed successfully");
            }
            return BadRequest("Invalid credentials, password not changed!");
        }

        [HttpGet]
        [ValidateModel]
        public async Task<IActionResult> GetAllUsers([FromQuery] String? filterOn, [FromQuery] String? filterQuery, [FromQuery] String? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var userDtoList = await userService.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(userDtoList);


        }

        //Getting all image of a user by user Id
        [HttpGet]
        [Route("UserImages/{id:Guid}")]
        [ValidateModel]    
        public async Task<IActionResult> GetAllUserImages([FromRoute] Guid id)
        {
            var imageList = await userService.GetAllUserImagesByIdAsync(id);
            if(imageList != null)
            {
                return Ok(imageList);
            }
            return BadRequest("Invalid request !!!");
        }

    }
}
