﻿namespace Hemiptera_Contracts.Project;
public record CreateProjectRequest(
    string Name,
    string Description,
    string? RepositoryLink,
    DateTime StartDateTime,
    DateTime? EndDateTime,
    int Status,
    int Type);