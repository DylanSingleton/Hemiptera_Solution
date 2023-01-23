namespace Hemiptera_Contracts.Authentication.Requests;
public record RegisterRequest(
    string Email,
    string ConfirmedEmail,
    string UserName,
    string Password,
    string ConfirmedPassword);