using FluentValidation;
using Hemiptera_API.Results;

namespace Hemiptera_API.Extensions
{
    public static class ValidationExtensions
    {
        public static ErrorResult<Error> Validate<T>(this T request, AbstractValidator<T> validator)
        {
            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                return new ErrorResult<Error>();
            }
            else
            {
                var validationErrors = validationResult.Errors
                    .Select(x => new Error(x.ErrorCode, x.ErrorMessage)).ToList();

                return new ErrorResult<Error>("Errors", validationErrors);
            }
        }
    }
}