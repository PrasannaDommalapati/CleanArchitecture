using Infrastructure.Service;
using Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IApplicantService, ApplicantService>();
        services.AddSingleton<IGetCurrentTimeService, GetCurrentTimeService>();
    }
}
