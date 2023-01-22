using FluentValidation;
using Hemiptera_Contracts.Authentication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemiptera_Contracts.Authentication.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator() 
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.ConfirmedEmail).EmailAddress().NotEmpty().Matches(x => x.Email);

            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(8)
                .Matches(@"[A-Z]+")
                .Matches(@"[a-z]+")
                .Matches(@"[0-9]+")
                .Matches(@"[\!\?\*\.]+");

            RuleFor(x => x.ConfirmedEmail).NotEmpty()
             .MinimumLength(8)
             .Matches(@"[A-Z]+")
             .Matches(@"[a-z]+")
             .Matches(@"[0-9]+")
             .Matches(@"[\!\?\*\.]+")
             .Matches(x => x.Password);
        }
    }
}
