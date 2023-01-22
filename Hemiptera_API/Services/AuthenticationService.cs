using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;

namespace Hemiptera_API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public ServiceResultWithPayload<AuthenticationResponse> Login(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public ServiceResultWithPayload<AuthenticationResponse> Register(LoginRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
