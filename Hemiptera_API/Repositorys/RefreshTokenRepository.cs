using Hemiptera_API.Models;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Services;
using Hemiptera_API.Services.Interfaces;
using System.Security.Cryptography;

namespace Hemiptera_API.Repositorys;
public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext context) : base(context) { }

    public RefreshToken GenerateRefreshToken(Guid userId)
    {
            var randomToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            return RefreshToken.From(userId, randomToken);
    }

    public void RevokeRefreshToken(Guid userId)
    {
        var tokenQuery = _context.RefreshTokens.FirstOrDefault(x => x.UserId == userId);
        if(tokenQuery != null)
        {
            _context.RefreshTokens.Remove(tokenQuery);
        }
    }

    public ServiceResult ValidateRefreshToken()
    {
        throw new NotImplementedException();
    }
}
