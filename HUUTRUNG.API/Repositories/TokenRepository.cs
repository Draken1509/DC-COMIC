using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HUUTRUNG_WEBAPI.Repositories
{
    public class TokenRepository:   ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // Create claims với key ngắn gọn
            var claims = new List<Claim>
            {
                new Claim("email", user.Email), // Thay ClaimTypes.Email bằng "email"
                new Claim("id", user.Id)       // Thay ClaimTypes.NameIdentifier bằng "id"
            };

            // Thêm các roles với key ngắn gọn
            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role)); // Thay ClaimTypes.Role bằng "role"
            }

            // Tạo key và credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tạo JWT token
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],       // Issuer
                configuration["Jwt:Audience"],    // Audience
                claims,                            // Claims
                expires: DateTime.Now.AddMinutes(30), // Thời gian hết hạn
                signingCredentials: credentials    // Chữ ký
            );

            // Trả về token dưới dạng string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
