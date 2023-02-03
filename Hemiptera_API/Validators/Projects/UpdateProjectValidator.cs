using FluentValidation;
using Hemiptera_Contracts.Project.Requests;

namespace Hemiptera_API.Validators.Projects;

public class UpdateProjectValidator : AbstractValidator<UpdateProjectRequest>
{
    public UpdateProjectValidator()
    {
        RuleFor(x => x.RepositoryLink)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.RepositoryLink))
            .WithMessage("Please ensure {PropertyName} is in the form of a URI");

        RuleFor(x => x.EndDateTime)
            .GreaterThan(x => x.StartDateTime);

        RuleFor(x => x.Status)
            .IsInEnum();

        RuleFor(x => x.Type)
            .IsInEnum();
    }
}