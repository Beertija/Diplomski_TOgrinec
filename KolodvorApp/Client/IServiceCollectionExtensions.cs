using Blazored.LocalStorage;
using KolodvorApp.Client.HttpServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;

namespace KolodvorApp.Client;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        IWebAssemblyHostEnvironment hostEnvironment, WebAssemblyHostConfiguration configuration)
    {
        InitializeHttpClient(services, hostEnvironment);
        InitializeServices(services);
        InitializeMudServices(services);
        services.AddBlazoredLocalStorage();

        InitializeAuthentication(services);

        services.AddLocalization();

        return services;
    }

    private static void InitializeHttpClient(IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
    {
        services.AddHttpClient("KolodvorApp.Server", (serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(hostEnvironment.BaseAddress);
        });

        // Supply HttpClient instances that include access tokens when making requests to the server project
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("KolodvorApp.Server"));
    }

    private static void InitializeServices(IServiceCollection services)
    {
        services.AddScoped<ITrainService, TrainService>();
        services.AddScoped<ITrainMaintenanceService, TrainMainenanceService>();
        services.AddScoped<IRouteService, RouteService>();
        services.AddScoped<IStationService, StationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITicketService, TicketService>();
    }

    private static void InitializeMudServices(IServiceCollection services)
    {
        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;

            config.SnackbarConfiguration.PreventDuplicates = false;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 5000;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });
    }

    private static void InitializeAuthentication(IServiceCollection services)
    {
        services.AddOptions();
        services.AddAuthorizationCore();
        services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
    }
}