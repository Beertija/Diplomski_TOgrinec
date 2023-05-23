using KolodvorApp.Persistance.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KolodvorApp.Persistance;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var context = new KolodvorAppContext(serviceProvider.GetRequiredService<DbContextOptions<KolodvorAppContext>>());

        context.Database.SetCommandTimeout(30);

        var applyChanges = false;

        //TODO: seed data checks & method calls

        if (applyChanges) await context.SaveChangesAsync();
    }
}