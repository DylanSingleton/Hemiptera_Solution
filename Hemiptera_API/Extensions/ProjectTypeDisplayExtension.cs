using Hemiptera_API.Models.Enums;
using System.Diagnostics;

namespace Hemiptera_API.Extensions
{
    public static class ProjectTypeDisplayExtension
    {
        public static string DisplayString(this ProjectType type) => type switch
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
