using FluentValidation;
using Hemiptera_Contracts.Authentication.Requests;

namespace Hemiptera_Contracts.Authentication.Validators;

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

    private bool HaveAtLeastOneSymbol(string password)
    {
        string acceptedSymbols = "!@#$%^&*()_-+={}[]|:;<>,.?/~`";

        bool hasSymbol = false;

        for (int i = 0; i < password.Length; i++)
        {
            char c = password[i];
            if (acceptedSymbols.Contains(c))
            {
                hasSymbol = true;
                break;
            }
        }

        return hasSymbol;
    }

    private bool HaveAtleastOneNumber(string password)
    {
        bool hasNumber = false;

        for (int i = 0; i < password.Length; i++)
        {
            char c = password[i];
            if (char.IsNumber(c))
            {
                hasNumber = true;
            }
        }

        return hasNumber;
    }
}