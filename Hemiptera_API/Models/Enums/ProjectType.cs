using System.ComponentModel.DataAnnotations;

namespace Hemiptera_API.Models.Enums
{
    public enum ProjectType
    {
        [Display(Name = "Web Application")]
        WebApplication,
        [Display(Name = "Mobile Application")]
        MobileApplication,
        [Display(Name = "Desktop Application")]
        DesktopApplication,
        [Display(Name = "System Integration")]
        SystemIntegration,
        [Display(Name = "API Development")]
        APIDevelopment,
        [Display(Name = "Cloud Based")]
        CloudBased,
        [Display(Name = "Data Science")]
        DataScience,
        [Display(Name = "Virtual Reality")]
        VirtualReality,
        [Display(Name = "IoT")]
        IoT,
        [Display(Name = "Embedded System")]
        EmbeddedSystem
    }
}
