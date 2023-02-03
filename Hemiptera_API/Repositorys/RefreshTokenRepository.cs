using Hemiptera_API.Models;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Results;
using Hemiptera_API.Services;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_API.Settings;
using Hemiptera_Contracts.Authentication.Responses;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Hemiptera_API.Repositorys;

public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext context) : base(context)
    {
    }

    public void RevokeRefreshToken(List<Claim> claims)
    {
        var userId = GetUserIdFromClaims(claims);
        var tokenQuery = _context.RefreshTokens.FirstOrDefault(x => x.UserId == userId);
        if (tokenQuery != null)
        {
            _context.RefreshTokens.Remove(tokenQuery);
        }
    }

    public void SetRefreshToken(List<Claim> claims, string token)
    {
        // TO:DO Encrypt refresh token
        var userId = GetUserIdFromClaims(claims);
        _context.RefreshTokens.Add(RefreshToken.From(userId, token));
        _context.SaveChanges();
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

    private Guid GetUserIdFromClaims(List<Claim> claims)
    {
        var userIdString = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier.ToString());
        return Guid.Parse(userIdString!.Value);
    }
}