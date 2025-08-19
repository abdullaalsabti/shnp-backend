namespace WebApplication1.Data.Entities;

public class RefreshToken
{
    public int TokenId { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(30);

    public bool IsRevoked { get; set; } = false;
    //The IsRevoked flag in your RefreshToken model is there to handle token invalidation before its natural expiration.

    // User logs out → You revoke their refresh token(s).
    // Password reset or security breach → All existing refresh tokens are revoked, forcing new logins.
    // Admin disables an account → Tokens can be revoked to immediately cut access.
}
