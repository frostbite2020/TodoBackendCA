using Application.Common.Interfaces;
using AutoMapper.Configuration;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<TodoCategory> TodoCategories { get; set; }
        public DbSet<UserProperty> UserProps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
