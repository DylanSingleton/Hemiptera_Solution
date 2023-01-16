using System.ComponentModel.DataAnnotations;

namespace Hemiptera_API.Models.Enums
{
    public enum ProjectStatus
    {
        [Display(Name = "New")]
        New,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "On Hold")]
        OnHold,
        [Display(Name = "Completed")]
        Completed,
        [Display(Name = "Cancelled")]
        Cancelled,
        [Display(Name = "Closed")]
        Closed,
        [Display(Name = "In Review")]
        InReview,
        [Display(Name = "Testing")]
        Testing,
        [Display(Name = "Deployment")]
        Deployment
    }
}
