using KolodvorApp.Domain.Entities;
using KolodvorApp.Persistance.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KolodvorApp.Persistance;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new KolodvorAppContext(serviceProvider.GetRequiredService<DbContextOptions<KolodvorAppContext>>());

        context.Database.SetCommandTimeout(30);

        if (!context.Trains.Any())
        {
            AddTrainsWithSomeMaintances(context);
        }
    }

    private static void AddTrainsWithSomeMaintances(KolodvorAppContext context)
    {
        context.Trains.AddRange(
                new Train
                {
                    Tag = "0112",
                    Capacity = 100,
                    Maintenances = new List<TrainMaintenance>
                    {
                        new TrainMaintenance()
                        {
                            Maintenance = "Popravak sjedišta",
                            Cost = 2000m
                        },
                        new TrainMaintenance()
                        {
                            Maintenance = "Popravak glavnog motora",
                            Cost = 30000m
                        }
                    }
                },
                new Train
                {
                    Tag = "1825",
                    Capacity = 210
                }
            );

        context.SaveChanges();
    }
}