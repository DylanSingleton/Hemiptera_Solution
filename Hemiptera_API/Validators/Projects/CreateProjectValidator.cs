using FluentValidation;
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
            RuleFor(x => x.Status).NotEmpty().LessThanOrEqualTo(9);
            RuleFor(x => x.Type).NotEmpty().LessThanOrEqualTo(10);
        }
}