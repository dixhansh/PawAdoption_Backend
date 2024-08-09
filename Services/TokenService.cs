using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PawAdoption_Backend.Models.Domain;
using PawAdoption_Backend.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PawAdoption_Backend.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public JwtResponseDto CreateJWTToken(User user, List<string> roles)
        {

            var claims = new List<Claim>();


            claims.Add(new Claim(ClaimTypes.Email, user.Email));


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var expiration = DateTime.UtcNow.AddMinutes(30);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                TokenType = "Bearer",
                Expiration = expiration
            };

        }
    }
}
