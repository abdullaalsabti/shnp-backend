namespace WebApplication1.Data.Entities;

public class UserAuth
{
    public int UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
