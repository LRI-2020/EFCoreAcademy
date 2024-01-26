using Microsoft.EntityFrameworkCore;

namespace EFCoreAcademy;

public class ApplicationDbContext : DbContext
{
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Professor> Professors { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>()
            .HasOne(e => e.Professor)
            .WithMany(e => e.Classes)
            .OnDelete(DeleteBehavior.NoAction);

        base.OnModelCreating(modelBuilder);
    }
    
}