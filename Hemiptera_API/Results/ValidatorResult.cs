using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hemiptera_API.Results
{
    public class ValidatorResult : Result
    {
        public string? Message { get; set; }

        public Dictionary<string, List<string>> Errors { get; set; }

        public ValidatorResult()
        {
            IsSuccessful = true;
        }

        public ValidatorResult(string message, Dictionary<string, List<string>> errors)
        {
            IsSuccessful = false;
            IsUnsuccessful = true;
            Errors = errors;
            Message = message;
        }

        public ValidatorResult(string message)
        {
            IsSuccessful = false;
            Message = message;
        }
    }
}