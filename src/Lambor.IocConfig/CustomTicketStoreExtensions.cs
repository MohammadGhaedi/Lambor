using System;
using Lambor.DataLayer.MSSQL;
using Lambor.Services.Identity;
using Lambor.ViewModels.Identity.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace Lambor.IocConfig
{
    public static class CustomTicketStoreExtensions
    {
        public static IServiceCollection AddCustomTicketStore(
            this IServiceCollection services, SiteSettings siteSettings)
        {
            // To manage large identity cookies
            var cookieOptions = siteSettings.CookieOptions;
            if (!cookieOptions.UseDistributedCacheTicketStore)
            {
                return services;
            }


            services.AddDistributedSqlServerCache(options =>
            {
                var cacheOptions = cookieOptions.DistributedSqlServerCacheOptions;
                options.ConnectionString = string.IsNullOrWhiteSpace(cacheOptions.ConnectionString) ?
                        siteSettings.GetMsSqlDbConnectionString() :
                        cacheOptions.ConnectionString;
                options.SchemaName = cacheOptions.SchemaName;
                options.TableName = cacheOptions.TableName;
            });
            services.AddScoped<ITicketStore, DistributedCacheTicketStore>();


            return services;
        }
}
}