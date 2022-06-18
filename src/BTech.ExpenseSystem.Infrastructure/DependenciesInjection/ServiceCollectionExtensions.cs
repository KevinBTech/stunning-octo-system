using BTech.ExpenseSystem.Domain.UseCases;
using BTech.ExpenseSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BTech.ExpenseSystem.Infrastructure.DependenciesInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ExpenseSystemContext>(
                options => options.UseSqlServer(connectionString))
                .AddScoped<ExpenseSystemContext>()

                .AddScoped(typeof(IWriteRepository<>), typeof(EfRepository<>))
                .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

            return services;
        }
    }
}