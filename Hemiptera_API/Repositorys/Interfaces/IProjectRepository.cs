using Hemiptera_API.Models;
using Hemiptera_API.Results;
using Hemiptera_Contracts.Projects.Requests;
using Hemiptera_Contracts.Users.Responses;

namespace Hemiptera_API.Services.Interfaces;

public interface IProjectRepository : IGenericRepository<Project>
{
    Result AssignUserToProject(UsersProjectsRequest request);
    Result RemoveUserFromProject(UsersProjectsRequest request);
    Result<List<User>> GetUsersInProject(Guid projectId);
    
}