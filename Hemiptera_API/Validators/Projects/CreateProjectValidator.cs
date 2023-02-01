using FluentValidation;
using Hemiptera_Contracts.Project.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemiptera_Contracts.Project.Validator;
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