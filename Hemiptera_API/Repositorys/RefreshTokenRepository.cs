using Hemiptera_API.Models;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Services;
using Hemiptera_API.Services.Interfaces;
using System.Security.Cryptography;

namespace Hemiptera_API.Repositorys;
public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext context) : base(context) { }

    public RefreshToken GenerateRefreshToken()
    {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            return RefreshToken.From(token);
    }

    public void RevokeRefreshToken()
    {
    }
}
