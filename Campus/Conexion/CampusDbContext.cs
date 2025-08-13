using Campus.Models;
using Microsoft.EntityFrameworkCore;

namespace Campus.Conexion
{
    public class CampusDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public CampusDbContext(DbContextOptions<CampusDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
