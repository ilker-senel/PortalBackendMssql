using Infrastructure.Data.Entities;
using Infrastructure.Data.EntityFramework;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddDataLayerExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
                     opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<UserRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<CategoryRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
