using DemoApplication.Areas.Client.Validation;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using DemoApplication.Services.Concretes;

namespace DemoApplication.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEmailService, SMTPService>();
            services.AddScoped<IUserActivationService, UserActivationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<CurrentUserAtributeValidation>();
            services.AddScoped<IBasketService, BasketService>();

        }
    }
}
