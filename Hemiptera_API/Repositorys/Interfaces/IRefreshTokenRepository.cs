using Hemiptera_API.Models;
using Hemiptera_API.Services.Interfaces;

namespace Hemiptera_API.Repositorys.Interfaces
{
    public interface  IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        RefreshToken GenerateRefreshToken();
        void RevokeRefreshToken();

    }
}
