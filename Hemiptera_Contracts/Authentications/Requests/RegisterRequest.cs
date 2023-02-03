namespace Hemiptera_Contracts.Authentications.Requests;
public record RegisterRequest(
    string Email,
    string ConfirmedEmail,
    string UserName,
    string Password,
    string ConfirmedPassword);