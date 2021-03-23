using Application.Common.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class UserManagementConfiguration : IEntityTypeConfiguration<UserProperties>
    {
        public void Configure(EntityTypeBuilder<UserProperties> builder)
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
