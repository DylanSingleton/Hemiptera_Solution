namespace Hemiptera_API.ServiceErrors
{
    public class ServiceError
    {
        public string ErrorMessage { get; set; }
        public ServiceError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
