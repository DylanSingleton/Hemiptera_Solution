using Hemiptera_API.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hemiptera_API.Settings
{
    public static class JwtSettings
    {
        public const bool ValidateIssuer = true;
        public const bool ValidateAudience = true;
        public const bool ValidateLifetime = true;
        public const bool ValidateIssuerSigningKey = true;
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static SecurityKey IssuerSigningKey { get; set; } 
        public const int MinuteLifetime = 15;
        public const int DayLifetime = 7;
    }
}