namespace Hemiptera_Contracts.Projects.Requests;
public record UpdateProjectRequest(
    string Name,
    string Description,
    string? RepositoryLink,
    DateTime StartDateTime,
    DateTime? EndDateTime,
    int Status,
    int Type);