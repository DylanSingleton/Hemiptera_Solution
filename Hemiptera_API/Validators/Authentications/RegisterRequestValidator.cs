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
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .DependentRules(() =>
                {
                    RuleFor(x => x.ConfirmedEmail)
                        .NotEmpty()
                        .Equal(x => x.Email);
                });

            RuleFor(x => x.UserName)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(50)
                .Must(HaveAtleastOneNumber)
                .When(x => !String.IsNullOrEmpty(x.Password))
                .WithMessage("Your password must ahve at least one number")
                .Must(HaveAtLeastOneSymbol)
                .WithMessage("Your password must have at least one symbol")
                .When(x => !String.IsNullOrEmpty(x.Password))
                .DependentRules(() =>
                    {
                        RuleFor(x => x.ConfirmedPassword)
                        .NotEmpty()
                        .Equal(x => x.Password);
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
}