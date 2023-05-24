using KolodvorApp.Persistance.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;

namespace KolodvorApp.Server.Config;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.ConfigureDataAccess(configuration);

        services.AddSingleton(environment.ContentRootFileProvider);

        services.ConfigureServices();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddEndpointsApiExplorer();

        services.AddFeatureManagement();

        return services;
    }

    private static IServiceCollection ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<KolodvorAppContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                options => options.EnableRetryOnFailure());
        });

        //TODO: Add repositories

        return services;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        // TODO: Add domain services

        return services;
    }
}