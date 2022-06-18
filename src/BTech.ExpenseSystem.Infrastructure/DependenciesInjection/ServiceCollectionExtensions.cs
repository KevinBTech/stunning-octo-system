using BTech.ExpenseSystem.Domain.UseCases;
using BTech.ExpenseSystem.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace BTech.ExpenseSystem.Infrastructure.DependenciesInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddSingleton(typeof(IWriteRepository<>), typeof(InMemoryRepository<>));
        }
    }
}