using KolodvorApp.Persistance;
using KolodvorApp.Persistance.DataContext;
using Microsoft.EntityFrameworkCore;

namespace KolodvorApp.Server.Config;

public static class WebApplicationExtensions
{
    public static WebApplication Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                o.OAuthAppName("Kolodvor Swagger UI");
            });
        }

        return app;
    }

    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            using var appContext = scope.ServiceProvider.GetRequiredService<KolodvorAppContext>();
            try
            {
                appContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        return app;
    }

    public static WebApplication SeedData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<KolodvorAppContext>();

            DbInitializer.Initialize(services);
        }

        return app;
    }
}