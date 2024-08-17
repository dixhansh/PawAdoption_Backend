using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using PawAdoption_Backend.Data;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;
using PawAdoption_Backend.Repositories;

namespace PawAdoption_Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly PawAdoptionDataContext pawAdoptionDataContext;
        private readonly ITokenService tokenService;
        private readonly IAuthRepository userRepository;

        public AuthService(UserManager<User> userManager, PawAdoptionDataContext pawAdoptionDataContext, ITokenService tokenService, IAuthRepository userRepository)
        {
            this.userManager = userManager;
            this.pawAdoptionDataContext = pawAdoptionDataContext;
            this.tokenService = tokenService;
            this.userRepository = userRepository;
        }

        public async Task<string?> RegisterAsync(User user, AdopterAddress adopterAddress, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                Role role = new Role();
                await userManager.AddToRoleAsync(user, Role.Adopter);
                adopterAddress.UserId = user.Id;
                adopterAddress.User = user;
                await userRepository.AddAddressAsync(adopterAddress);
                return ("User registered successfully");
            }
            return null;
        }


        public async Task<JwtResponseDto?> ValidateUserAsync(LoginRequest loginRequest)
        {
            //Getting user from database
            var user = await userManager.FindByEmailAsync(loginRequest.Email);

            if (user != null)
            { 
                //validating Password
                var isPassValid = await userManager.CheckPasswordAsync(user, loginRequest.Password);

                if (isPassValid)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {

                        //Creating the JWT Token 
                        var jwtRenposedto = tokenService.CreateJWTToken(user, roles.ToList());

                        jwtRenposedto.Id = user.Id;
                        jwtRenposedto.FirstName = user.FirstName;
                        jwtRenposedto.LastName = user.LastName;
                        jwtRenposedto.Email = user.Email;
                        jwtRenposedto.DateOfBirth = user.DateOfBirth;
                        jwtRenposedto.Role = roles;

                        return jwtRenposedto;

                    }
                }
            }

            return null;
        }
    }
}
