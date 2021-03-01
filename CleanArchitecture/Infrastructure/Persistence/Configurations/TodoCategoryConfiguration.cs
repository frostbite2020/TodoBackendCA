using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class TodoCategoryConfiguration : IEntityTypeConfiguration<TodoCategory>
    {
        public void Configure(EntityTypeBuilder<TodoCategory> builder)
        {
            builder.Property(t => t.CategoryTitle)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
