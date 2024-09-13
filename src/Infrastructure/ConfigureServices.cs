using Domain;
using Infrastructure.Service;
using Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddSingleton<IGetCurrentTimeService, GetCurrentTimeService>();
        services.AddSingleton<IEmailService, EmailService>();
        services.AddScoped<IApplicantService, ApplicantService>();
        services.AddScoped<IUserManagementService, UserManagementService>();
        services.AddScoped<RoleSeedService>();
    }
}
