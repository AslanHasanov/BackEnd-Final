using AspNetCore.IServiceCollection.AddIUrlHelper;
using DemoApplication.Database;
using DemoApplication.Infrastructure.Configurations;
using DemoApplication.Options;
using DemoApplication.Services.Abstracts;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
         

            services.AddHttpContextAccessor();

            services.AddUrlHelper();


            services.ConfigureMvc();

            services.ConfigureDatabase(configuration);

            services.ConfigureOptions(configuration);

            services.ConfigureFluentValidatios(configuration);

            services.RegisterCustomServices(configuration);
        }
    }
}
