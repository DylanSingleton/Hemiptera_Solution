using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;

namespace Hemiptera_API.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ServiceResultWithPayload<AuthenticationResponse>> LoginAsync(LoginRequest request);
        Task<ServiceResultWithPayload<AuthenticationResponse>> Register(RegisterRequest request);
    }
}
