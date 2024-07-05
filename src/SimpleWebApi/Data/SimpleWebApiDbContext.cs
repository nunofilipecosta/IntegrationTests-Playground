using Microsoft.EntityFrameworkCore;

namespace SimpleWebApi.Data;

public class SimpleWebApiDbContext : DbContext
{
    public SimpleWebApiDbContext (DbContextOptions<SimpleWebApiDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MovieEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);

    }

    public DbSet<SimpleWebApi.Model.Movie> Movies { get; set; } = default!;
}
