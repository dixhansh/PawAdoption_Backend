namespace PawAdoption_Backend.Models.DTO
{
    public class JwtResponseDto
    {
        public string Token { get; set; }
        public string TokenType { get; set; } = "Bearer"; // Default value for Bearer tokens
        public DateTime? Expiration { get; set; }
    }
}
