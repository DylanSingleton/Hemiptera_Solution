using Hemiptera_API.Models;
using Hemiptera_API.Results;
using Hemiptera_API.Services;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Authentication.Responses;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace Hemiptera_API.Repositorys.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    void RevokeRefreshToken(List<Claim> claims);

    void SetRefreshToken(List<Claim> claims, string token);

    Result ValidateRefreshToken(string token);
}