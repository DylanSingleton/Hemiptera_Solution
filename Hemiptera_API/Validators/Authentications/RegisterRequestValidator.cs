using FluentValidation;
using Hemiptera_Contracts.Authentication.Requests;

namespace Hemiptera_API.Validators.Authentications;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithErrorCode("Email")
            .EmailAddress().WithErrorCode("Email")
            .DependentRules(() =>
            {
                RuleFor(x => x.ConfirmedEmail)
                    .Equal(x => x.Email).WithErrorCode("Email");
            });

        RuleFor(x => x.UserName)
            .NotEmpty().WithErrorCode("Username");

        RuleFor(x => x.Password)
            .NotNull().WithErrorCode("Password")
            .DependentRules(() =>
            {
                RuleFor(x => x.Password)
                  .MinimumLength(8).WithErrorCode("Password")
                  .MaximumLength(25).WithErrorCode("Password")
                  .Must(HaveAtleastOneNumber).WithErrorCode("Password").WithMessage("{PropertyName} must contain at least one number")
                  .Must(HaveAtLeastOneSymbol).WithErrorCode("Password").WithMessage("{PropertyName} must contain at least one symbol")
                  .DependentRules(() =>
                  {
                      RuleFor(x => x.ConfirmedPassword)
                          .Equal(x => x.Password).WithErrorCode("Password");
                  });
            });
    }

    private static bool HaveAtLeastOneSymbol(string password)
    {
        const string acceptedSymbols = "!@#$%^&*()_-+={}[]|:;<>,.?/~`";

        return password.Any(c => acceptedSymbols.Contains(c));
    }

    private static bool HaveAtleastOneNumber(string password)
    {
        return password.Any(char.IsNumber);
    }
}