using Microsoft.AspNetCore.Identity;

namespace HUUTRUNG_WEBAPI.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
