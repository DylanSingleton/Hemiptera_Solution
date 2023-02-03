using Hemiptera_API.Results;
using System.Security.Claims;
using Hemiptera_Contracts.Authentications.Requests;

namespace Hemiptera_API.Services.Interfaces;

public interface IAuthenticationRepository
{
    Task<Result<List<Claim>>> LoginAsync(LoginRequest request);

    Task<Result<List<Claim>>> Register(RegisterRequest request);
}