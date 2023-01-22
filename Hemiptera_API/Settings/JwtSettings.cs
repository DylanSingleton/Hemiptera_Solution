using Hemiptera_API.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hemiptera_API.Settings
{
    public class JwtSettings
    {
        public static bool ValidateIssuer = true;
        public static bool ValidateAudience = true;
        public static bool ValidateLifetime = true;

        public static bool ValidateIssuerSigningKey = true;
        public static string Issuer { get; set; } = string.Empty;
        public static string Audience { get; set; } = string.Empty;
        public static SecurityKey IssuerSigningKey;
        public static int Lifetime = 15;
    }
}