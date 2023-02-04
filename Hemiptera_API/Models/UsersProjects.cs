using Hemiptera_Contracts.Projects.Requests;

namespace Hemiptera_API.Models;

public class UsersProjects
{
    public Guid UserId { get; set; }
    public  User User { get; set; }

    public Guid ProjectId { get; set; }
    public Project Project { get; set; }

    private UsersProjects(Guid userId, Guid projectId)
    {
        
    }
    private static UsersProjects Create(
        Guid userId,
        Guid projectId)
    {
        return new UsersProjects(
            userId,
            projectId);
    }

    public static UsersProjects From(Guid userId, Guid projectId)
    {
        return Create(userId, projectId);
    }

    public static UsersProjects From(UsersProjectsRequest request)
    {
        return Create(request.userId, request.projectId);
    }
}