using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawAdoption_Backend.CustomActionFilters;

namespace PawAdoption_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //get user(adopter) by id 
        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            var userResponseDto = await userService.GetUserByIdAsync(id);
        }

    }
}
