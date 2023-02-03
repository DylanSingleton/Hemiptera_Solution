using FluentValidation;
using Hemiptera_Contracts.Authentication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemiptera_Contracts.Authentication.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithErrorCode("Email")
            .EmailAddress().WithErrorCode("Email");

        RuleFor(x => x.Password)
            .NotEmpty().WithErrorCode("Password");
    }
}