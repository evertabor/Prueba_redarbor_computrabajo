using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Persistence.Contexts;
using CleanArchitectrure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectrure.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services)
        {
            services.AddDbContext<DataBaseContext>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
