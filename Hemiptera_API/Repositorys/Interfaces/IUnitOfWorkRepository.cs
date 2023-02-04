using Hemiptera_API.Repositorys.Interfaces;

namespace Hemiptera_API.Services.Interfaces;

public interface IUnitOfWorkRepository : IDisposable
{
    IProjectRepository Project { get; }
    IRefreshTokenRepository RefreshToken { get; }
    ITicketsRepository Ticket { get; }

    void Save();
}