using Business.Services;
using Business.Services.Interface;
using Business.Utilities.Mapping.Interface;
using Business.Utilities.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Business.Utilities.Security.Auth.Interface;
using Business.Utilities.Security.Auth;
using Business.Utilities.Validation.Interface;
using Business.Utilities.Validation;
using Microsoft.AspNetCore.Http;

namespace Business
{
    public static class ServiceExtensions
    {
        public static void AddBusinessLayerExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMapperHelper, MapperHelper>();
            services.AddSingleton<IValidationHelper, ValidationHelper>();
            services.AddSingleton<IJwtTokenHelper, JwtTokenHelper>();
            services.AddSingleton<IHashingHelper, HashingHelper>();
            //services.AddSingleton<IMailHelper, MailHelper>();

            services.AddScoped<IClaimHelper, ClaimHelper>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddSingleton<IMapperHelper, MapperHelper>();


        }

    }
}
