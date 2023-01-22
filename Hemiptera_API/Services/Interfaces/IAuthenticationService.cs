using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;

namespace Hemiptera_API.Services.Interfaces
{
    public interface IAuthenticationService
    {
        ServiceResultWithPayload<AuthenticationResponse> Login(LoginRequest request);
        ServiceResultWithPayload<AuthenticationResponse> Register(LoginRequest request);
    }
}
