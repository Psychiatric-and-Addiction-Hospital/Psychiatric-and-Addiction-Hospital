using Application.Common.Interfaces.UpLoad;
using Psychiatric_and_Addiction_Hospital.Services.FileStorage;

namespace Psychiatric_and_Addiction_Hospital.Dependency
{
    public static class DependencyInjectionServices
    {
        public static void AddDependencyWebServices(this IServiceCollection services)
        {
            services.AddScoped<IFileStorage, LocalFileStorage>();
        }
    }
}