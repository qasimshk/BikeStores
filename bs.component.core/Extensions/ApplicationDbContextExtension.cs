using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;
using bs.component.sharedkernal.Exceptions;

namespace bs.component.core.Extensions
{
    public static class ApplicationDbContextExtension
    {
        public static IServiceCollection AddApplicationDbContextExtension<TDbContext>(this IServiceCollection services, IConfiguration configuration) where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(options =>
                {
                    if (string.IsNullOrEmpty(configuration.GetConnectionString("DefaultConnection")))
                    {
                        throw new NotFoundException("SQL Server connection string now found");
                    }

                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    });
                });

            services.AddSingleton<IDbConnection>(new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
