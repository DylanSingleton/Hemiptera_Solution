using Hemiptera_API.Models;
using Hemiptera_API.Services.Interfaces;

namespace Hemiptera_API.Services
{
    public class ProjectService : GenericService<Project>, IProjectService
    {
        public ProjectService(ApplicationDbContext context): base(context) { }

        public IEnumerable<Project> GetAllProjectsByType()
        {
            throw new NotImplementedException();
        }
    }
}
