using Hemiptera_API.Models;
using Hemiptera_API.Services;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Authentication.Responses;

namespace Hemiptera_API.Repositorys.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    void RevokeRefreshToken(Guid userId);
}