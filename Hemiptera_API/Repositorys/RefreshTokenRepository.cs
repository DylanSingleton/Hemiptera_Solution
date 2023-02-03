using Hemiptera_API.Models;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Results;
using Hemiptera_API.Services;
using System.Security.Claims;

namespace Hemiptera_API.Repositorys;

public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext context) : base(context)
    {
    }

    private bool RevokeRefreshToken(Guid userId)
    {
        var tokenQuery = _context.RefreshTokens.FirstOrDefault(x => x.UserId == userId);
        if (tokenQuery == null) return false;
        _context.RefreshTokens.Remove(tokenQuery);
        _context.SaveChanges();
        return true;

    }

    public void SetRefreshToken(List<Claim> claims, string token)
    {
        // TO:DO Encrypt refresh token
        var userId = GetUserIdFromClaims(claims);
        RevokeRefreshToken(userId);
        _context.RefreshTokens.Add(RefreshToken.From(userId, token));
        _context.SaveChanges();
    }

    public Result RevokeUserRefreshToken(Guid userId)
    {
        return RevokeRefreshToken(userId) ? new SuccessResult() : new ErrorResult("Invalid refresh token");
    }

    public Result ValidateRefreshToken(string token)
    {
        var tokenQuery = _context.RefreshTokens.FirstOrDefault(x => x.Token == token);
        if (tokenQuery is not null && tokenQuery.ExpiryDateTime > DateTime.Now)
        {
            return new SuccessResult();
        }
        return new ErrorResult("Invalid refresh token");
    }

    private static Guid GetUserIdFromClaims(List<Claim> claims)
    {
        var userIdString = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier.ToString());
        return Guid.Parse(userIdString!.Value);
    }
}