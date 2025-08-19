using WebApplication1.Data.Entities;

namespace WebApplication1.Auth;

public static class AuthUtils
{
    public static bool CheckStillValidRefreshToken(RefreshToken? refreshToken)
    {
        if (refreshToken == null || refreshToken.IsRevoked || refreshToken.ExpiresAt < DateTime.UtcNow) return false;
        return true;
    }
}
