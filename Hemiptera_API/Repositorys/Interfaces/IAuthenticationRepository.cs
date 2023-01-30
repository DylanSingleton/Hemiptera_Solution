using Hemiptera_API.Results;
using Hemiptera_Contracts.Authentication.Requests;
using Hemiptera_Contracts.Authentication.Responses;
using System.Security.Claims;

namespace Hemiptera_API.Services.Interfaces
{
    public interface IAuthenticationRepository
    {
        Task<Result<List<Claim>>> LoginAsync(LoginRequest request);

        Task<Result<List<Claim>>> Register(RegisterRequest request);
    }
}