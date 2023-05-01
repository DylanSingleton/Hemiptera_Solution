using Hemiptera_API.Models.Enums;
using System.Diagnostics;

namespace Hemiptera_API.Extensions;

public static class TicketPriorityDisplayExtension
{
    public static string DisplayString(this TicketPriority priority) => priority switch
    {
        TicketPriority.Low => "Low",
        TicketPriority.Medium => "Medium",
        TicketPriority.High => "High",
        TicketPriority.Urgent => "Urgent",
        _ => throw new UnreachableException()
    };
}