using Hemiptera_API.Models.Enums;
using Hemiptera_Contracts.Tickets.Requests;

namespace Hemiptera_API.Models;

public class Ticket
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }

    public Guid ReporterId { get; set; }
    public User Reporter { get; set; }

    public Guid AssignedToId { get; set; }
    public User AssignedTo { get; set; }

    public TicketPriority Priority { get; set; }
    public TicketStatus Status { get; set; }

    // Project Relationship
    public Guid ProjectId { get; set; }

    public Project Project { get; set; }
    // Platform Table

    private Ticket(
        Guid id,
        string title,
        string summary,
        string description,
        Guid reporterId,
        Guid assignedToId,
        TicketPriority priority,
        TicketStatus status,
        Guid projectId)
    {
        Id = id;
        Title = title;
        Summary = summary;
        Description = description;
        ReporterId = reporterId;
        AssignedToId = assignedToId;
        Priority = priority;
        Status = status;
        ProjectId = projectId;
    }

    private static Ticket Create(
        string title,
        string summary,
        string description,
        Guid reporterId,
        Guid assignedToId,
        TicketPriority priority,
        TicketStatus status,
        Guid projectId,
        Guid? id = null)
    {
        return new Ticket(
            id ?? Guid.NewGuid(),
            title,
            summary,
            description,
            reporterId,
            assignedToId,
            priority,
            status,
            projectId);
    }

    public static Ticket From(CreateTicketRequest request)
    {
        return Create(
            request.title,
            request.summary,
            request.description,
            request.reporterId,
            request.assignedToId,
            (TicketPriority)request.priority,
            (TicketStatus)request.status,
            request.projectId);
    }

    public static Ticket From(Guid id, UpdateTicketRequest request)
    {
        return Create(
               request.title,
               request.summary,
               request.description,
               request.reporterId,
               request.assignedToId,
               (TicketPriority)request.priority,
               (TicketStatus)request.status,
               request.projectId,
               id);
    }
}