namespace Hemiptera_Contracts.Tickets.Responses;

public record TicketResponse(
    string Title,
    string Summary,
    string Description,
    Guid ReporterId,
    Guid AssignedToId,
    string Priority,
    string Status,
    Guid ProjectId);