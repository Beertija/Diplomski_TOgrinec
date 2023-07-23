using KolodvorApp.Domain;
using KolodvorApp.Domain.Entities;
using KolodvorApp.Domain.Services;
using KolodvorApp.Persistance;
using KolodvorApp.Persistance.DataContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;

namespace KolodvorApp.Server.Config;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.ConfigureDataAccess(configuration);

        services.AddSingleton(environment.ContentRootFileProvider);

        services.ConfigureServices();

        services.ConfigureAuthentication(configuration);

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
        services.AddScoped<IRepository<Station>, Repository<Station>>();
        services.AddScoped<IRepository<RouteStation>, Repository<RouteStation>>();
        services.AddScoped<IRepository<Domain.Entities.Route>, Repository<Domain.Entities.Route>>();
        services.AddScoped<IRepository<User>, Repository<User>>();

        return services;
    }

    private static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<ITrainService, TrainService>();
        services.AddScoped<ITrainMaintenanceService, TrainMaintenanceService>();
        services.AddScoped<ITrainCategoryService, TrainCategoryService>();
        services.AddScoped<IStationService, StationService>();
        services.AddScoped<IRouteService, RouteService>();
        services.AddScoped<IRouteCalculatorService, RouteCalculatorService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    private static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                        .GetBytes(configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }
}