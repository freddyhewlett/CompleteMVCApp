using FH.App.Extensions;
using FH.Business.Interfaces;
using FH.Business.Notifications;
using FH.Business.Services;
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

            services.AddScoped<INotificator, Notificator>();
            services.AddScoped<IDeveloperService, DeveloperService>();
            services.AddScoped<IGameService, GameService>();

            return services;
        }
    }
}
