using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataAccess;

public class LBetContext: DbContext
{
    private readonly string _connectionString;
    public LBetContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    internal LBetContext()
    {
        _connectionString = "Server=localhost;Database=master;Trusted_Connection=True;\n";
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
        base.OnConfiguring(optionsBuilder);
    }
    
    public override int SaveChanges()
    {
        IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();

        foreach(EntityEntry entry in entries)
        {
            if(entry.State == EntityState.Added)
            {
                if (entry.Entity is Entity e)
                {
                    e.IsActive = true;
                    e.CreatedAt = DateTime.UtcNow;
                }
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is Entity e)
                {
                    e.UpdatedAt = DateTime.UtcNow;
                }
            }
        }

        return base.SaveChanges();
    }

    public DbSet<Team> Teams { get; set; }
}