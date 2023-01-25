using Hemiptera_API.Models.Enums;
using Hemiptera_API.Settings;
using Hemiptera_Contracts.Project.Requests;

namespace Hemiptera_API.Models;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryDateTime { get; set; }
    public bool IsExpired { get; set; }

    private RefreshToken(
        Guid id,
        string token,
        DateTime expiryDateTime,
        bool isExpired)
    {
        Id = id;
        Token = token;
        ExpiryDateTime = expiryDateTime;
        IsExpired = isExpired;
    }

    public static RefreshToken Create(
        string token,
        Guid? id = null)
    {
        return new RefreshToken(
            id ?? Guid.NewGuid(),
            token,
            DateTime.Now.AddDays(JwtSettings.DayLifetime),
            false);
    }

    public static RefreshToken From(string token)
    {
        return Create(token);
    }
}
