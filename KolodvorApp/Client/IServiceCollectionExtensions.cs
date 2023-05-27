using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace KolodvorApp.Client;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        IWebAssemblyHostEnvironment hostEnvironment, WebAssemblyHostConfiguration configuration)
    {
        InitializeHttpClient(services, hostEnvironment);
        InitializeServices(services);

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
}