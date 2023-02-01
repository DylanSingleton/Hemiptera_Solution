using FluentValidation;
using Hemiptera_API.Results;

namespace Hemiptera_API.Extensions
{
    public static class ValidatorResultValidateExtension
    {
        public static ValidatorResult Validate<T>(this T request, AbstractValidator<T> validator)
        {
            try
            {
                var validationResult = validator.Validate(request);
                if (validationResult.IsValid)
                {
                    return new ValidatorResult();
                }
                else
                {
                    var validationErrors = validationResult.Errors.Select(x => new Error(x.PropertyName, x.ErrorMessage)).ToList();
                    return new ValidatorResult("Errors", validationErrors.ToList());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during validation: " + ex.Message);
                Console.WriteLine("Stack trace: " + ex.StackTrace);
                return new ValidatorResult("Errors", new Error("An error occurred during validation."));
            }
        }
    }
}