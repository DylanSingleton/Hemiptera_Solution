using Hemiptera_API.Models;

namespace Hemiptera_API.Services.Interfaces;

public interface IProjectRepository : IGenericRepository<Project>
{
    IEnumerable<Project> GetAllProjectsByType();
}