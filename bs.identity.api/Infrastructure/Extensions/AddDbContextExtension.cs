using bs.identity.api.Infrastructure.Configuration;
using bs.identity.domain.Entities;
using bs.identity.infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace bs.identity.api.Infrastructure.Extensions
{
    public static class AddDbContextExtension
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var identityconfiguration = configuration.GetSection(nameof(IdentityConfiguration)).Get<IdentityConfiguration>();

            // install Microsoft.EntityFrameworkCore.SqlServer
            services.AddDbContext<EmployeeDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<Employee, IdentityRole>()
                .AddEntityFrameworkStores<EmployeeDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            //Password Strength Setting
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = identityconfiguration.PasswordRequireDigit;
                options.Password.RequiredLength = identityconfiguration.PasswordRequiredLength;
                options.Password.RequireNonAlphanumeric = identityconfiguration.PasswordRequireNonAlphanumeric;
                options.Password.RequireUppercase = identityconfiguration.PasswordRequireUppercase;
                options.Password.RequireLowercase = identityconfiguration.PasswordRequireLowercase;
                options.Password.RequiredUniqueChars = identityconfiguration.PasswordRequiredUniqueChars;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = identityconfiguration.LockoutMaxFailedAccessAttempts;
                options.Lockout.AllowedForNewUsers = identityconfiguration.LockoutAllowedForNewUsers;

                // User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = identityconfiguration.UserRequireUniqueEmail;
            });

            return services;
        }
    }
}
