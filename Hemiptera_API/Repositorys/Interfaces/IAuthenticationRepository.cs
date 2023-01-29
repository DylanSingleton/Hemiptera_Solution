using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;
using System.Security.Claims;

namespace Hemiptera_API.Services.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<OperationResultWithPayload<List<Claim>>> LoginAsync(LoginRequest request);
        Task<OperationResultWithPayload<List<Claim>>> Register(RegisterRequest request);
    }
}
