using PawAdoption_Backend.Models.Enum;

namespace PawAdoption_Backend.Models.DTO
{
    public class JwtResponseDto
    {
        public string Token { get; set; }
        public string TokenType { get; set; } = "Bearer"; // Default value for Bearer tokens
        public DateTime? Expiration { get; set; }

        //returning additional information about the logged in user
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string Email { get; set; }

        public string LastName { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public IList<string?> Role { get; set; }
    }
}
