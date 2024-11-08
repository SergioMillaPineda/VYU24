using Microsoft.Extensions.DependencyInjection;
using WorkersManagementAdminMode.Library.Contracts;

namespace WorkersManagementAdminMode.Library.Impl.DependencyInjectionExtension
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IITWorkerService, ITWorkerService>();
        }
    }
}
