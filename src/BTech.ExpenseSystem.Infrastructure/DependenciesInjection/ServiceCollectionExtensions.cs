using BTech.ExpenseSystem.Domain.UseCases;
using BTech.ExpenseSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BTech.ExpenseSystem.Infrastructure.DependenciesInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<ExpenseSystemContext>(
                options => options.UseInMemoryDatabase("ExpenseSystem"));
            services.AddScoped<ExpenseSystemContext>();
            return services.AddScoped(typeof(IWriteRepository<>), typeof(EfRepository<>));
        }
    }
}