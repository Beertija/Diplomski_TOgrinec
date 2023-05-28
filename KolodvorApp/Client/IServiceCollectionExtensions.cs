﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
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

        services.AddLocalization();

        return services;
    }

    private static void InitializeHttpClient(IServiceCollection services, IWebAssemblyHostEnvironment hostEnvironment)
    {
        services.AddHttpClient("KolodvorApp.ServerAPI", (serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(hostEnvironment.BaseAddress);
        });

        // Supply HttpClient instances that include access tokens when making requests to the server project
        services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("KolodvorApp.ServerAPI"));
    }

    private static void InitializeServices(IServiceCollection services)
    {
        //TODO: add services
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
}