namespace Hemiptera_Contracts.Project;
record UpsertProjectRequest(string Name,
    string Description,
    string? RepositoryLink,
    DateTime StartDateTime,
    DateTime? EndDateTime,
    int Status,
    int Type);