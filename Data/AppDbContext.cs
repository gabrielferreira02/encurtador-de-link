using EncurtadorUrl.Entities;
using Microsoft.EntityFrameworkCore;

namespace EncurtadorUrl.Data;

internal class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<UrlEntity> Urls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrlEntity>(entity =>
        {
            entity.HasIndex(u => u.Code).IsUnique();
            entity.HasIndex(u => u.Code).IsUnique();
        });
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    //     base.OnConfiguring(optionsBuilder);
    // }
}
