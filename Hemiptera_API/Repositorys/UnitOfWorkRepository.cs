using Hemiptera_API.Models;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Services.Interfaces;

namespace Hemiptera_API.Services;

public class UnitOfWorkRepository : IUnitOfWorkRepository
{
    private readonly ApplicationDbContext _context;
    public IProjectRepository Project { get; }
    public IRefreshTokenRepository RefreshToken { get; }
    public  ITicketsRepository Ticket { get; }

    public UnitOfWorkRepository(
        ApplicationDbContext context,
        IProjectRepository projectRepository,
        IRefreshTokenRepository refreshTokenRepository,
        ITicketsRepository ticketsRepository)
    {
        _context = context;
        Project = projectRepository;
        RefreshToken = refreshTokenRepository;
        Ticket = ticketsRepository;
    }

    private bool _disposed;

    public void Save()
    {
        _context.SaveChanges();
    }

    public void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}