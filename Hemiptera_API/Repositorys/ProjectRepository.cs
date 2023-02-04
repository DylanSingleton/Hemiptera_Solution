using Hemiptera_API.Models;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Projects.Requests;
using Hemiptera_Contracts.Users.Responses;
using Microsoft.EntityFrameworkCore;

namespace Hemiptera_API.Services;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext context): base(context) { }

    public Result AssignUserToProject(UsersProjectsRequest request)
    {
        var user = _context.Users.Find(request.userId);
        if(user is null) return new NotFoundResult(typeof(User), request.userId.ToString());
        
        var project = _context.Projects.Find(request.projectId);
        if(project is null) return new NotFoundResult(typeof(Project), request.projectId.ToString());

        var usersProjects = UsersProjects.From(request);
        usersProjects.User = user;
        usersProjects.Project = project;
        
        var result = _context
            .UsersProjects
            .Add(usersProjects);

        if (result.State == EntityState.Added)
        {
            return new SuccessResult();
        }
        //TODO Possibly make error message more readable
        return new ErrorResult($"Error adding user with ID: {request.userId} to project with ID: {request.projectId}");
    }

    public Result RemoveUserFromProject(UsersProjectsRequest request)
    {
        var user = _context.Users.Find(request.userId);
        if(user is null) return new NotFoundResult(typeof(User), request.userId.ToString());
        
        var project = _context.Projects.Find(request.projectId);
        if(project is null) return new NotFoundResult(typeof(Project), request.projectId.ToString());

        var usersProjects = UsersProjects.From(request);
        usersProjects.User = user;
        usersProjects.Project = project;
        
        var result = _context
            .UsersProjects
            .Remove(usersProjects);

        if (result.State == EntityState.Deleted)
        {
            return new SuccessResult();
        }
        //TODO Possibly make error message more readable
        return new ErrorResult($"Error adding user with ID: {request.userId} to project with ID: {request.projectId}");    }

    public Result<List<User>> GetUsersInProject(Guid projectId)
    {
        var usersInProject = _context
            .UsersProjects
            .Where(x => x.ProjectId == projectId)
            .Select(x => x.User).ToList();

        if (!usersInProject.Any()) return new NotFoundResult<List<User>>(typeof(User));
        return new SuccessResult<List<User>>(usersInProject);
    }
}
