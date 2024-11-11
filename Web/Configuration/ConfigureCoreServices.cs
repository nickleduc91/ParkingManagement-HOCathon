using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Web.Interfaces;
using Web.Services;

namespace Web.Configuration;

public static class ConfigureCoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddScoped<IParkingGarageService, ParkingGarageService>();
        services.AddScoped<IParkingGarageViewModelService, ParkingGarageViewModelService>();
        services.AddScoped<IParkingSpotService, ParkingSpotService>();
        services.AddScoped<ICarRegisterFormService, CarRegisterFormService>();

        return services;
    }
}
