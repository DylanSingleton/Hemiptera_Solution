using Hemiptera_API.Models;

namespace Hemiptera_API.Services.Interfaces
{
    public interface IProjectService : IGenericService<Project>
    {
        IEnumerable<Project> GetAllProjectsByType();
    }
}
