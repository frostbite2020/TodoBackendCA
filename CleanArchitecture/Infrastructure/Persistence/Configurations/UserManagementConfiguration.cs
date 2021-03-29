using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserManagementConfiguration : IEntityTypeConfiguration<UserProperty>
    {
        public void Configure(EntityTypeBuilder<UserProperty> builder)
        {
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(x => x.Username)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(200);
        }
    }
}
