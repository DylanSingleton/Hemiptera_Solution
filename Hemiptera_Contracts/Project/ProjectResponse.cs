namespace Hemiptera_Contracts.Project;
record ProjectResponse(string Name,
    string Description,
    string? RepositoryLink,
    DateTime StartDateTime,
    DateTime? EndDateTime,
    string Status,
    string Type);


