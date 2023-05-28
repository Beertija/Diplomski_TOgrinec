using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KolodvorApp.Persistance.DataContext;

public sealed class KolodvorAppContextFactory : IDesignTimeDbContextFactory<KolodvorAppContext>
{
    public KolodvorAppContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<KolodvorAppContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Kolodvor;Trusted_Connection=True");

        return new KolodvorAppContext(builder.Options);
    }
}