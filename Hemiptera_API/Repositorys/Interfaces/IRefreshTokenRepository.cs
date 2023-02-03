using Hemiptera_API.Models;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using System.Security.Claims;

namespace Hemiptera_API.Repositorys.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    void SetRefreshToken(List<Claim> claims, string token);
    Result RevokeUserRefreshToken(Guid userId);
    Result ValidateRefreshToken(string token);
}