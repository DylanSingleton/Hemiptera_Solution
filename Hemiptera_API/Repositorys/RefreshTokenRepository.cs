using Hemiptera_API.Models;
using Hemiptera_API.Repositorys.Interfaces;
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
    public RefreshTokenRepository(ApplicationDbContext context) : base(context) { }

    public void RevokeRefreshToken(Guid userId)
    {
        var tokenQuery = _context.RefreshTokens.FirstOrDefault(x => x.UserId == userId);
        if(tokenQuery != null)
        {
            _context.RefreshTokens.Remove(tokenQuery);
        }
    }
}
