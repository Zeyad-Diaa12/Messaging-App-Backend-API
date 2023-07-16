using SocialApi.Models;

namespace SocialApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
