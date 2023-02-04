namespace Hemiptera_Contracts.Projects.Requests;

public record UsersProjectsRequest(
    Guid userId,
    Guid projectId);