namespace Hemiptera_Contracts.Project;
public record UpsertProjectRequest(string Name,
    string Description,
    string? RepositoryLink,
    DateTime StartDateTime,
    DateTime? EndDateTime,
    int Status,
    int Type);