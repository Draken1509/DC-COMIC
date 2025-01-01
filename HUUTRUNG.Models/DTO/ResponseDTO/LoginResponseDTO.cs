namespace HUUTRUNG.Models.DTO.ResponseDTO
{
    public class LoginResponseDTO
    {
        public string JwtToken { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
