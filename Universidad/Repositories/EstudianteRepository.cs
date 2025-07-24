using Universidad.Models;

namespace Universidad.Repositories
{
    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly UniversidadDbContext context;
        public EstudianteRepository(UniversidadDbContext context)
        {
            this.context = context;
        }
        public Estudiante GetEstudiante(int id)
        {
            return context.Estudiantes.FirstOrDefault(e => e.id == id);// SELECT e.id, e.nombre... FROM Estudiante e WHERE e.id = @id
        }

        public IEnumerable<Estudiante> GetEstudiantes()
        {
            
            return context.Estudiantes.ToList();
        }
    }
}
