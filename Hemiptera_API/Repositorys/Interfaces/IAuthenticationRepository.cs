using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;

namespace Hemiptera_API.Services.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<ServiceResultWithPayload<string>> LoginAsync(LoginRequest request);
        Task<ServiceResultWithPayload<string>> Register(RegisterRequest request);
    }
}
