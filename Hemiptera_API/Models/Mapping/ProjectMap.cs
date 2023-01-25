using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hemiptera_API.Models.Mapping
{
    public class RefreshTokenMap
    {
        public RefreshTokenMap(EntityTypeBuilder<RefreshToken> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.Id);
            entityTypeBuilder.Property(x => x.Token).IsRequired();
            entityTypeBuilder.Property(x => x.ExpiryDateTime).IsRequired();
            entityTypeBuilder.Property(x => x.IsExpired).IsRequired();
        }
    }
}
