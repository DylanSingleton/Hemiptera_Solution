using Hemiptera_API.Models.Enums;
using Hemiptera_API.Settings;
using Hemiptera_Contracts.Project.Requests;

namespace Hemiptera_API.Models;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryDateTime { get; set; }

    private RefreshToken(
        Guid id,
        Guid userId,
        string token,
        DateTime expiryDateTime)
    {
        Id = id;
        Token = token;
        UserId = userId;
        ExpiryDateTime = expiryDateTime;
    }

    private static RefreshToken Create(
        Guid userId,
        string token,
        Guid? id = null)
    {
        return new RefreshToken(
            id ?? Guid.NewGuid(),
            userId,
            token,
            DateTime.Now.AddDays(JwtSettings.DayLifetime));
    }

    public static RefreshToken From(Guid userId, string token)
    {
        return Create(userId, token);
    }
}
