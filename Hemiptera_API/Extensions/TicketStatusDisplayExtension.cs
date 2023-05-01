using Hemiptera_API.Models.Enums;
using System.Diagnostics;

namespace Hemiptera_API.Extensions;

public static class TicketStatusDisplayExtension
{
    public static string DisplayString(this TicketStatus status) => status switch
    {
        TicketStatus.Open => "Open",
        TicketStatus.InProgress => "In Progress",
        TicketStatus.Resolved => "Resolved",
        TicketStatus.Closed => "Closed",
        _ => throw new UnreachableException()
    };
}