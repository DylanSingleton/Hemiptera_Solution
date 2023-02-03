namespace Hemiptera_Contracts.Projects.Responses;
public record ProjectResponse(
    string Name,
    string Description,
    string? RepositoryLink,
    DateTime StartDateTime,
    DateTime? EndDateTime,
    string Status,
    string Type);


