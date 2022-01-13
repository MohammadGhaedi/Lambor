using System;
using Lambor.DataLayer.MSSQL;
using Lambor.Services.Contracts.Identity;
using Lambor.ViewModels.Identity.Settings;
using DNTCommon.Web.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Lambor.IocConfig
{
    public static class DbContextOptionsExtensions
    {
        public static IServiceCollection AddConfiguredDbContext(
            this IServiceCollection serviceCollection, SiteSettings siteSettings)
        {
            serviceCollection.AddConfiguredMsSqlDbContext(siteSettings);
            return serviceCollection;
        }

        /// <summary>
        /// Creates and seeds the database.
        /// </summary>
        public static void InitializeDb(this IServiceProvider serviceProvider)
        {
            serviceProvider.RunScopedService<IIdentityDbInitializer>(identityDbInitialize =>
            {
                identityDbInitialize.Initialize();
                identityDbInitialize.SeedData();
            });
        }
    }
}