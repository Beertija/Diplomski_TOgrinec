using Microsoft.EntityFrameworkCore;

namespace KolodvorApp.Persistance.DataContext;

public sealed class KolodvorAppContext : DbContext
{
    public KolodvorAppContext(DbContextOptions<KolodvorAppContext> options) : base(options) { }

    //TODO: DbSets

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //TODO: Models
    }
}