using Hemiptera_API.Models;
using Hemiptera_API.Repositorys.Interfaces;
using Hemiptera_API.Services;

namespace Hemiptera_API.Repositorys;

public class TicketsRepository : GenericRepository<Ticket>, ITicketsRepository
{
    public TicketsRepository(ApplicationDbContext context) : base(context)
    {
    }
}