using Business.Services;
using Business.Services.Interface;
using Business.Utilities.Mapping.Interface;
using Business.Utilities.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class ServiceExtensions
    {
        public static void AddBusinessLayerExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddSingleton<IMapperHelper, MapperHelper>();


        }

    }
}
