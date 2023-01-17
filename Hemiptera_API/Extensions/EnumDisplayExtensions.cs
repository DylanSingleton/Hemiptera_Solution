using Hemiptera_API.Models.Enums;
using System.Diagnostics;

namespace Hemiptera_API.Extensions
{
    public static class EnumDisplayExtensions
    {
        public static string GetProjectStatusDisplayString(this ProjectStatus status) => status switch
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

        public static string GetProjectTypeDisplayString(this ProjectType type) => type switch
        {
            ProjectType.WebApplication => "Web Application",
            ProjectType.MobileApplication => "Mobile Application",
            ProjectType.DesktopApplication => "Desktop Application",
            ProjectType.SystemIntegration => "System Integration",
            ProjectType.APIDevelopment => "API Development",
            ProjectType.CloudBased => "Cloud Based",
            ProjectType.DataScience => "Data Science",
            ProjectType.VirtualReality => "Virtual Reality",
            ProjectType.IoT => "IoT",
            ProjectType.EmbeddedSystem => "Embedded System",
            _ => throw new UnreachableException()
        };
    }
}
