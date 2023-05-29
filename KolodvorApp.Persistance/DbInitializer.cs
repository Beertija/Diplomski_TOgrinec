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

        if (!context.TrainCategories.Any())
        {
            AddTrainCategories(context);
        }

        if (!context.Trains.Any())
        {
            AddTrainsWithSomeMaintances(context);
        }
    }

    private static void AddTrainCategories(KolodvorAppContext context)
    {
        context.TrainCategories.AddRange(
                new TrainCategory
                {
                    Name = "Vagoni drugog razreda"
                },
                new TrainCategory
                {
                    Name = "Vagoni prvog razreda"
                },
                new TrainCategory
                {
                    Name = "Vagoni za prijevoz bicikla"
                },
                new TrainCategory
                {
                    Name = "Putnički vlak"
                },
                new TrainCategory
                {
                    Name = "Brzi vlak"
                }
            );

        context.SaveChanges();
    }

    private static void AddTrainsWithSomeMaintances(KolodvorAppContext context)
    {
        context.Trains.AddRange(
                new Train
                {
                    Tag = "112",
                    Capacity = 100,
                    Maintenances = new List<TrainMaintenance>
                    {
                        new TrainMaintenance
                        {
                            Maintenance = "Popravak sjedišta",
                            Cost = 2000m
                        },
                        new TrainMaintenance
                        {
                            Maintenance = "Popravak glavnog motora",
                            Cost = 30000m
                        }
                    },
                    Categories = new List<Contains>
                    {
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.First(x => x.Name.Equals("Brzi vlak")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.First(x => x.Name.Equals("Vagoni prvog razreda")).Id
                        }
                    }
                },
                new Train
                {
                    Tag = "1825",
                    Capacity = 210,
                    Categories = new List<Contains> 
                    {
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.First(x => x.Name.Equals("Vagoni drugog razreda")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.First(x => x.Name.Equals("Putnički vlak")).Id
                        }
                    }
                }
            );

        context.SaveChanges();
    }
}