using Microsoft.AspNetCore.Identity;

namespace DWDWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
