using Hemiptera_API.ServiceErrors;

namespace Hemiptera_API.Services
{
    public class ServiceResult
    {
        public bool IsFailure { get; set; }
        public List<ServiceError>? Errors { get; set; }
    }
}
