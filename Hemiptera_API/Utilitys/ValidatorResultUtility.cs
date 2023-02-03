using FluentValidation;
using Hemiptera_API.Results;

namespace Hemiptera_API.Utilitys
{
    public static class ValidatorResultUtility
    {
        public static ValidatorResult Validate<T>(T request, AbstractValidator<T> validator)
        {
            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                return new ValidatorResult();
            }
            
            var validationErrors = new Dictionary<string, List<string>>();
                foreach (var error in validationResult.Errors)
                {
                    if (validationErrors.ContainsKey(error.ErrorCode))
                    {
                        validationErrors[error.ErrorCode].Add(error.ErrorMessage);
                    }
                    else
                    {
                        validationErrors.Add(error.ErrorCode, new List<string> { error.ErrorMessage });
                    }
                }
                return new ValidatorResult("Errors", validationErrors);
            }
        }
    }
