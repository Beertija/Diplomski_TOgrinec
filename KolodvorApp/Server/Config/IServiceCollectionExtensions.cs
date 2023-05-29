using KolodvorApp.Domain;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Domain.Services;
using KolodvorApp.Persistance;
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
        services.AddSwaggerGen();

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

        services.AddScoped<IRepository<Train>, Repository<Train>>();
        services.AddScoped<IRepository<TrainMaintenance>, Repository<TrainMaintenance>>();
        services.AddScoped<IRepository<TrainCategory>, Repository<TrainCategory>>();

        return services;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<ITrainService, TrainService>();
        services.AddScoped<ITrainMaintenanceService, TrainMaintenanceService>();
        services.AddScoped<ITrainCategoryService, TrainCategoryService>();

        return services;
    }
}