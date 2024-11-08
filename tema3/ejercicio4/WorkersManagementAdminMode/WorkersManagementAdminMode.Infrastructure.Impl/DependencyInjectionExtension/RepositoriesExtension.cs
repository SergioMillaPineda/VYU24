using Microsoft.Extensions.DependencyInjection;
using WorkersManagementAdminMode.Infrastructure.Contracts;
using WorkersManagementAdminMode.Infrastructure.Impl.DbContexts;

namespace WorkersManagementAdminMode.Infrastructure.Impl.DependencyInjectionExtension
{
    public static class RepositoriesExtension
    {
        public static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IITWorkersRepository, ITWorkersRepository>();
            serviceCollection.AddScoped<WorkersManagementAdminModeContext>();
        }
    }
}
