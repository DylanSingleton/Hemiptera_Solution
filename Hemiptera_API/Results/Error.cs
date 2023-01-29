namespace Hemiptera_API.Results
{
    public class Error
    {
        public string? Code { get; set; }
        public string Details { get; set; }
        public Error(string details)
        {
            Code = null;
            Details = details;
        }

        public Error(string code, string details)
        {
            Code = code;
            Details = details;
        }
    }
}
