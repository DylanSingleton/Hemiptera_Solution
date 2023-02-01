using Hemiptera_API.Models.Enums;
using System.Diagnostics;

namespace Hemiptera_API.Extensions;

public static class ProjectStatusDisplayExtension
{
    public static string DisplayString(this ProjectStatus status) => status switch
    {
        ProjectStatus.New => "New",
        ProjectStatus.InProgress => "In Progress",
        ProjectStatus.OnHold => "On Hold",
        ProjectStatus.Completed => "Completed",
        ProjectStatus.Cancelled => "Cancelled",
        ProjectStatus.Closed => "Closed",
        ProjectStatus.InReview => "In Review",
        ProjectStatus.Testing => "Testing",
        ProjectStatus.Deployment => "Deployment",
        _ => throw new UnreachableException()
    };
}