using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hemiptera_API.Results
{
    public class ValidatorResult : Result
    {
        public string? Message { get; set; }
        public ICollection<Error>? Errors { get; set; }

        public ValidatorResult()
        {
            IsSuccessful = true;
        }

        public ValidatorResult(string message, List<Error> errors)
        {
            IsSuccessful = false;
            IsUnsuccessful = true;
            Errors = errors;
            Message = message;
        }

        public ValidatorResult(string message, Error error)
        {
            IsSuccessful = false;
            Errors = new List<Error>();
            Errors.Add(error);
            Message = message;
        }
    }
}