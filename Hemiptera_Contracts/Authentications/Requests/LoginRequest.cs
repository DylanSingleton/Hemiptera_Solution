namespace Hemiptera_Contracts.Authentications.Requests;
public record LoginRequest(
    string Email,
    string Password);