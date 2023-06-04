using FirstApiProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstApiProject.DAL.EFCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Car>cars { get; set; }
    public DbSet<Brand> brands { get; set; }
    public DbSet<Color> colors { get; set; }
}
