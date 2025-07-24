using Microsoft.EntityFrameworkCore;
using Universidad.Models;

namespace Universidad.Repositories
{
    public class UniversidadDbContext : DbContext
    {
        public UniversidadDbContext(DbContextOptions<UniversidadDbContext> options) : base(options)
        {
        }
        public DbSet<Estudiante> Estudiantes { get; set; }
        
    }
}
