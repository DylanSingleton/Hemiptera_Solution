namespace Hemiptera_Contracts.Tickets.Requests;

public record CreateTicketRequest(
    string title,
    string summary,
    string description,
    Guid reporterId,
    Guid assignedToId,
    int priority,
    int status,
    Guid projectId);