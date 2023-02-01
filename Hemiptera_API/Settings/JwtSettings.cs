using Microsoft.IdentityModel.Tokens;

namespace Hemiptera_API.Settings;

/// <summary>
/// JWT settings class used to define the configuration for JWT authentication.
/// </summary>
public static class JwtSettings
{
    /// <summary>
    /// Validate issuer flag indicating if the issuer should be validated.
    /// </summary>
    public const bool ValidateIssuer = true;

    /// <summary>
    /// Validate audience flag indicating if the audience should be validated.
    /// </summary>
    public const bool ValidateAudience = true;

    /// <summary>
    /// Validate lifetime flag indicating if the lifetime should be validated.
    /// </summary>
    public const bool ValidateLifetime = true;

    /// <summary>
    /// Validate issuer signing key flag indicating if the issuer signing key should be validated.
    /// </summary>
    public const bool ValidateIssuerSigningKey = true;

    private static string? _issuer;

    /// <summary>
    /// Issuer property to hold the value of the issuer.
    /// The value for this property is set in Program.cs based on the environment.
    /// </summary>
    public static string Issuer
    {
        get => GetIssuer();
        set => SetIssuer(value);
    }

    private static string? _audience;

    /// <summary>
    /// Audience property to hold the value of the audience.
    /// The value for this property is set in Program.cs based on the environment.
    /// </summary>
    public static string Audience
    {
        get => GetAudience();
        set => SetAudience(value);
    }

    private static SecurityKey? _issuerSigningKey;

    /// <summary>
    /// IssuerSigningKey property to hold the value of the issuer signing key.
    /// The value for this property is set in Program.cs based on the environment.
    /// </summary>
    public static SecurityKey IssuerSigningKey
    {
        get => GetIssuerSigningKey();
        set => SetIssuerSigningKey(value);
    }

    /// <summary>
    /// Lifetime in minutes.
    /// </summary>
    public const int MinuteLifetime = 15;

    /// <summary>
    /// Lifetime in days.
    /// </summary>
    public const int DayLifetime = 7;

    /// <summary>
    /// Method to retrieve the issuer.
    /// </summary>
    /// <returns>The issuer value</returns>
    private static string GetIssuer()
    {
        return _issuer
            ?? throw new InvalidOperationException
            ($"The 'Issuer' property of class '{nameof(JwtSettings)}' must be set before use.");
    }

    /// <summary>
    /// Method to set the issuer.
    /// </summary>
    /// <param name="value">The value to set the issuer to</param>
    private static void SetIssuer(string value)
    {
        _issuer = value ?? throw new ArgumentNullException
              ($"The value for the 'IssuerSigningKey' property in class '{nameof(JwtSettings)}' cannot be null.");
    }

    /// <summary>
    /// Method to retrieve the audience.
    /// </summary>
    /// <returns>The audience value</returns>
    private static string GetAudience()
    {
        return _audience
            ?? throw new InvalidOperationException
            ($"The 'Audience' property of class '{nameof(JwtSettings)}' must be set before use.");
    }

    /// <summary>
    /// Method to set the audience.
    /// </summary>
    /// <param name="value">The value to set the audience to</param>
    private static void SetAudience(string value)
    {
        _audience = value ?? throw new ArgumentNullException
                ($"The value for the 'IssuerSigningKey' property in class '{nameof(JwtSettings)}' cannot be null.");
    }

    /// <summary>
    /// Method to retrieve the issuer signing key.
    /// </summary>
    /// <returns>The issuer value</returns>
    private static SecurityKey GetIssuerSigningKey()
    {
        return _issuerSigningKey
            ?? throw new InvalidOperationException
            ($"The 'IssuerSigningKey' property of class '{nameof(JwtSettings)}' must be set before use.");
    }

    /// <summary>
    /// Method to set the the issuer signing key.
    /// </summary>
    /// <param name="value">The value to set the issuer signing key to</param>
    private static void SetIssuerSigningKey(SecurityKey value)
    {
        _issuerSigningKey = value ?? throw new ArgumentNullException
                ($"The value for the 'IssuerSigningKey' property in class '{nameof(JwtSettings)}' cannot be null.");
    }
}