using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;

namespace Hemiptera_API.Services.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<ServiceResultWithPayload<AuthenticatedResponse>> LoginAsync(LoginRequest request);
        Task<ServiceResultWithPayload<AuthenticatedResponse>> Register(RegisterRequest request);
    }
}
