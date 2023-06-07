using FH.App.Extensions;
using FH.Business.Interfaces;
using FH.Data.Context;
using FH.Data.Repositories;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

namespace FH.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<DataDbContext>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, CurrencyValidationAttributeProvider>();


            return services;
        }
    }
}
