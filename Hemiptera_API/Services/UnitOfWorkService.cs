using Hemiptera_API.Models;
using Hemiptera_API.Services.Interfaces;

namespace Hemiptera_API.Services
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly ApplicationDbContext _context;
        public IProjectService ProjectService { get; }

        public UnitOfWorkService(ApplicationDbContext context,
            IProjectService projectService)
        {
            _context = context;
            ProjectService = projectService;
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
}
