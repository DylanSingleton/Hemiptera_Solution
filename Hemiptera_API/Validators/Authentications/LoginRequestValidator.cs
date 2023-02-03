using FluentValidation;
using Hemiptera_Contracts.Authentications.Requests;

namespace Hemiptera_API.Validators.Authentications;

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