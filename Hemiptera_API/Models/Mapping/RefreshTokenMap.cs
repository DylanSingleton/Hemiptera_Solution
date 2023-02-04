using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hemiptera_API.Models.Mapping;

public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);
        entityTypeBuilder.Property(x => x.Token).IsRequired();
        entityTypeBuilder.Property(x => x.ExpiryDateTime).IsRequired();

        entityTypeBuilder.HasOne(x => x.User)
            .WithOne(x => x.RefreshToken)
            .HasForeignKey<User>(x => x.RefreshTokenId);
    }
}