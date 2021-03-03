using Application.Common.Interfaces;
using Infrastructure.Files;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Identity.Entities;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}