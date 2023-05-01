using FluentValidation;
using Hemiptera_API.Models.Enums;
using Hemiptera_API.Utilitys;
using Hemiptera_Contracts.Projects.Requests;

namespace Hemiptera_API.Validators.Projects;

public class CreateProjectValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.RepositoryLink).Must(
            uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
        .When(x => !string.IsNullOrEmpty(x.RepositoryLink))
        .WithMessage("Please ensure {PropertyName} is in the form of a URI");

        RuleFor(x => x.StartDateTime).NotEmpty();
        RuleFor(x => x.EndDateTime).GreaterThan(x => x.StartDateTime);

        // Status validation message
        var statusMessage = EnumValidationMessageUtility.GetEnumValidationMessage<ProjectStatus>();

        // Status must be one of the enum values
        RuleFor(x => x.Status)
        .NotEmpty().WithMessage(statusMessage).WithErrorCode("Status")
        .GreaterThanOrEqualTo(0).WithMessage(statusMessage).WithErrorCode("Status")
        .LessThanOrEqualTo(sizeof(ProjectStatus)).WithMessage(statusMessage).WithErrorCode("Status");

        // Type validation message
        var typeMessage = EnumValidationMessageUtility.GetEnumValidationMessage<ProjectType>();

        // Type must be one of the enum values
        RuleFor(x => x.Type).NotEmpty().WithMessage(typeMessage).WithErrorCode("Type")
            .GreaterThanOrEqualTo(0).WithMessage(typeMessage).WithErrorCode("Type")
            .LessThanOrEqualTo(sizeof(ProjectType)).WithMessage(typeMessage).WithErrorCode("Type");
    }
}