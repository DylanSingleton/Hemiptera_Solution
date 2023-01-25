using Microsoft.AspNetCore.Identity;

namespace Hemiptera_API.Models
{
    public class User : IdentityUser<Guid>
    {
        public Guid? RefreshTokenId { get; set; }
        public virtual RefreshToken? RefreshToken { get; set; }
    }
}
