using Microsoft.EntityFrameworkCore;
using Notebook.Models;

namespace Notebook.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // public DbSet<User> Users { get; set; } = null!;
    // public DbSet<UserAttribute> UserAttributes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relacje i konfiguracja – zrobimy w Etapie 3
        base.OnModelCreating(modelBuilder);
    }
}