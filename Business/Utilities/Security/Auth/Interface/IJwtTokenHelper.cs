using Infrastructure.Data.Entities;

namespace Business.Utilities.Security.Auth.Interface
{
    public interface IJwtTokenHelper
    {
        Token CreateAccessToken(User user, string refreshToken);

    }
}
