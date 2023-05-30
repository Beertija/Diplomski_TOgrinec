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

        if (!context.Stations.Any())
        {
            AddStations(context);
        }

        if (!context.Routes.Any())
        {
            AddRoutes(context);
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
                },
                new TrainCategory
                {
                    Name = "Vagon s mjestima za osobe s invaliditetom"
                }
            );

        context.SaveChanges();
    }

    private static void AddTrainsWithSomeMaintances(KolodvorAppContext context)
    {
        context.Trains.AddRange(
                new Train
                {
                    Tag = "6410",
                    Capacity = 300,
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
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Putnički vlak")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Vagoni drugog razreda")).Id
                        }
                    }
                },
                new Train
                {
                    Tag = "3606",
                    Capacity = 210,
                    Categories = new List<Contains>
                    {
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Putnički vlak")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Vagoni drugog razreda")).Id
                        }
                    }
                },
                new Train
                {
                    Tag = "2302",
                    Capacity = 260,
                    Categories = new List<Contains>
                    {
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Putnički vlak")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Vagoni drugog razreda")).Id
                        }
                    },
                    Maintenances = new List<TrainMaintenance>
                    {
                        new TrainMaintenance
                        {
                            Maintenance = "Popravak sjedišta",
                            Cost = 25000m
                        },
                        new TrainMaintenance
                        {
                            Maintenance = "Popravak sirene",
                            Cost = 5000m
                        },
                        new TrainMaintenance
                        {
                            Maintenance = "Zamjena prednjih kotača",
                            Cost = 20000m
                        }
                    },
                },
                new Train
                {
                    Tag = "6436",
                    Capacity = 210,
                    Categories = new List<Contains>
                    {
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Putnički vlak")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Vagoni drugog razreda")).Id
                        }
                    }
                },
                new Train
                {
                    Tag = "580",
                    Capacity = 300,
                    Categories = new List<Contains>
                    {
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Brzi vlak")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Vagoni prvog razreda")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Vagoni drugog razreda")).Id
                        }
                    }
                },
                new Train
                {
                    Tag = "2206",
                    Capacity = 270,
                    Categories = new List<Contains>
                    {
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Putnički vlak")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Vagoni za prijevoz bicikla")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Vagon s mjestima za osobe s invaliditetom")).Id
                        },
                        new Contains
                        {
                            TrainCategoryId = context.TrainCategories.Single(x => x.Name.Equals("Vagoni drugog razreda")).Id
                        }
                    }
                }
            );

        context.SaveChanges();
    }

    private static void AddStations(KolodvorAppContext context)
    {
        context.Stations.AddRange(
                new Station
                {
                    Name = "Mursko Središće"
                },
                new Station
                {
                    Name = "Vratišinec"
                },
                new Station
                {
                    Name = "Novo Selo Rok"
                },
                new Station
                {
                    Name = "Čakovec-Buzovec"
                },
                new Station
                {
                    Name = "Čakovec"
                },
                new Station
                {
                    Name = "Dunjkovec"
                },
                new Station
                {
                    Name = "Macinec"
                },
                new Station
                {
                    Name = "Mala Subotica"
                },
                new Station
                {
                    Name = "Čehovec"
                },
                new Station
                {
                    Name = "Donji Kraljevec"
                },
                new Station
                {
                    Name = "Donji Mihaljevec"
                },
                new Station
                {
                    Name = "Kotoriba"
                },
                new Station
                {
                    Name = "Varaždin"
                },
                new Station
                {
                    Name = "Vidovec"
                },
                new Station
                {
                    Name = "Cerje-Tužno"
                },
                new Station
                {
                    Name = "Stražnjevec"
                },
                new Station
                {
                    Name = "Ivanec"
                },
                new Station
                {
                    Name = "Kuljevčica"
                },
                new Station
                {
                    Name = "Lepoglava"
                },
                new Station
                {
                    Name = "Golubovec"
                },
                new Station
                {
                    Name = "Zbelava"
                },
                new Station
                {
                    Name = "Jalžabet"
                },
                new Station
                {
                    Name = "Novakovec"
                },
                new Station
                {
                    Name = "Martijanec"
                },
                new Station
                {
                    Name = "Ludbreg"
                },
                new Station
                {
                    Name = "Čukovec"
                },
                new Station
                {
                    Name = "Rasinja"
                },
                new Station
                {
                    Name = "Kunovec-Subotica"
                },
                new Station
                {
                    Name = "Koprivnica"
                },
                new Station
                {
                    Name = "Drnje"
                },
                new Station
                {
                    Name = "Botovo"
                },
                new Station
                {
                    Name = "Bregi"
                },
                new Station
                {
                    Name = "Novigrad Podravski"
                },
                new Station
                {
                    Name = "Virje"
                },
                new Station
                {
                    Name = "Đurđevac"
                },
                new Station
                {
                    Name = "Kalinovac"
                },
                new Station
                {
                    Name = "Sirova Katalena"
                },
                new Station
                {
                    Name = "Kloštar"
                },
                new Station
                {
                    Name = "Pitomača"
                },
                new Station
                {
                    Name = "Vukosavljevica"
                },
                new Station
                {
                    Name = "Špišić-Bukovica"
                },
                new Station
                {
                    Name = "Virovitica Grad"
                },
                new Station
                {
                    Name = "Virovitica"
                },
                new Station
                {
                    Name = "Mučna Reka"
                },
                new Station
                {
                    Name = "Sokolovac"
                },
                new Station
                {
                    Name = "Lepavina"
                },
                new Station
                {
                    Name = "Carevdar"
                },
                new Station
                {
                    Name = "Vojakovački Kloštar"
                },
                new Station
                {
                    Name = "Majurec"
                },
                new Station
                {
                    Name = "Križevci"
                },
                new Station
                {
                    Name = "Repinec"
                },
                new Station
                {
                    Name = "Gradec"
                },
                new Station
                {
                    Name = "Gradec Stajalište"
                },
                new Station
                {
                    Name = "Vrbovec"
                },
                new Station
                {
                    Name = "Božjakovina"
                },
                new Station
                {
                    Name = "Dugo Selo"
                },
                new Station
                {
                    Name = "Sesvetski Kraljevec"
                },
                new Station
                {
                    Name = "Sesvete"
                },
                new Station
                {
                    Name = "Sesvetska Sopnica"
                },
                new Station
                {
                    Name = "Čulinec"
                },
                new Station
                {
                    Name = "Trnava"
                },
                new Station
                {
                    Name = "Zagreb Borongaj"
                },
                new Station
                {
                    Name = "Maksimir"
                },
                new Station
                {
                    Name = "Zagreb Glavni Kolodvor"
                },
                new Station
                {
                    Name = "Poljanka"
                },
                new Station
                {
                    Name = "Brezovljani"
                },
                new Station
                {
                    Name = "Škrinjari"
                },
                new Station
                {
                    Name = "Sveti Ivan Žabno"
                },
                new Station
                {
                    Name = "Haganj"
                },
                new Station
                {
                    Name = "Remetinec Križevački"
                },
                new Station
                {
                    Name = "Lubena"
                },
                new Station
                {
                    Name = "Cirkvena"
                },
                new Station
                {
                    Name = "Hrsovo"
                },
                new Station
                {
                    Name = "Rovišće"
                },
                new Station
                {
                    Name = "Žabjak"
                },
                new Station
                {
                    Name = "Klokočevac"
                },
                new Station
                {
                    Name = "Stare Plavnice"
                },
                new Station
                {
                    Name = "Bjelovar"
                },
                new Station
                {
                    Name = "Markovac"
                },
                new Station
                {
                    Name = "Grginac"
                },
                new Station
                {
                    Name = "Grginac Novi"
                },
                new Station
                {
                    Name = "Veliko Trojstvo"
                },
                new Station
                {
                    Name = "Mišulinovac"
                },
                new Station
                {
                    Name = "Paulovac"
                },
                new Station
                {
                    Name = "Zid Katalena"
                }
            );

        context.SaveChanges();
    }

    private static void AddRoutes(KolodvorAppContext context)
    {
        context.Routes.AddRange(
                new Route
                {
                    IsDaily = true,
                    Cost = 7.17m,
                    FirstStation = context.Stations.Single(x => x.Name.Equals("Koprivnica")).Id,
                    LastStation = context.Stations.Single(x => x.Name.Equals("Zagreb Glavni Kolodvor")).Id,
                    RouteStations = new List<RouteStation>
                    {
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Koprivnica")).Id,
                            DepartureTime = new TimeOnly(11, 15),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Mučna Reka")).Id,
                            ArrivalTime = new TimeOnly(11, 20),
                            DepartureTime = new TimeOnly(11, 21),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Sokolovac")).Id,
                            ArrivalTime = new TimeOnly(11, 24),
                            DepartureTime = new TimeOnly(11, 24),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Lepavina")).Id,
                            ArrivalTime = new TimeOnly(11, 28),
                            DepartureTime = new TimeOnly(11, 29),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Carevdar")).Id,
                            ArrivalTime = new TimeOnly(11, 34),
                            DepartureTime = new TimeOnly(11, 34),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Vojakovački Kloštar")).Id,
                            ArrivalTime = new TimeOnly(11, 37),
                            DepartureTime = new TimeOnly(11, 37),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Majurec")).Id,
                            ArrivalTime = new TimeOnly(11, 40),
                            DepartureTime = new TimeOnly(11, 40),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Križevci")).Id,
                            ArrivalTime = new TimeOnly(11, 45),
                            DepartureTime = new TimeOnly(11, 47),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Repinec")).Id,
                            ArrivalTime = new TimeOnly(11, 53),
                            DepartureTime = new TimeOnly(11, 54),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Gradec Stajalište")).Id,
                            ArrivalTime = new TimeOnly(11, 58),
                            DepartureTime = new TimeOnly(11, 59),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Vrbovec")).Id,
                            ArrivalTime = new TimeOnly(12, 06),
                            DepartureTime = new TimeOnly(12, 08),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Božjakovina")).Id,
                            ArrivalTime = new TimeOnly(12, 18),
                            DepartureTime = new TimeOnly(12, 19),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Dugo Selo")).Id,
                            ArrivalTime = new TimeOnly(12, 25),
                            DepartureTime = new TimeOnly(12, 27),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Sesvetski Kraljevec")).Id,
                            ArrivalTime = new TimeOnly(12, 33),
                            DepartureTime = new TimeOnly(12, 33),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Sesvete")).Id,
                            ArrivalTime = new TimeOnly(12, 38),
                            DepartureTime = new TimeOnly(12, 39),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Sesvetska Sopnica")).Id,
                            ArrivalTime = new TimeOnly(12, 42),
                            DepartureTime = new TimeOnly(12, 42),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Čulinec")).Id,
                            ArrivalTime = new TimeOnly(12, 45),
                            DepartureTime = new TimeOnly(12, 45),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Trnava")).Id,
                            ArrivalTime = new TimeOnly(12, 47),
                            DepartureTime = new TimeOnly(12, 47),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Maksimir")).Id,
                            ArrivalTime = new TimeOnly(12, 50),
                            DepartureTime = new TimeOnly(12, 51),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        },
                        new RouteStation
                        {
                            StationId = context.Stations.Single(x => x.Name.Equals("Zagreb Glavni Kolodvor")).Id,
                            ArrivalTime = new TimeOnly(12, 57),
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        }
                    },
                    RouteTrains = new List<RouteTrain>
                    {
                        new RouteTrain
                        {
                            TrainId = context.Trains.Single(x => x.Tag.Equals("2206")).Id
                        }
                    }
                }
            );

        context.SaveChanges();
    }
}