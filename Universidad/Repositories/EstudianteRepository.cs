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

        public void AddEstudiante(Estudiante estudiante)
        {
            if(estudiante == null)
                throw new ArgumentNullException(nameof(estudiante), "El estudiante no puede ser nulo.");
            context.Estudiantes.Add(estudiante);
            // Cuando se ejecute SaveChanges, se ejecutará algo como: INSERT INTO Estudiante (nombre, apellido, carrera, email) VALUES (@nombre, @apellido, @carrera, @email)
        }

        public Estudiante GetEstudiante(int id)
        {
            return context.Estudiantes.FirstOrDefault(e => e.id == id);// SELECT e.id, e.nombre... FROM Estudiante e WHERE e.id = @id
        }

        public IEnumerable<Estudiante> GetEstudiantes()
        {
            
            return context.Estudiantes.ToList();
        }

        public bool Guardar()
        {
            // La DDBB retorna 1 si se guardó correctamente, 0 si no se guardó nada, -1 si hubo un error
            return context.SaveChanges() >= 0; // Si se guardó al menos un registro, se retorna true
        }

        public void UpdateEstudiante(Estudiante estudiante)
        {
            // No es necesario hacer nada aquí, ya que Entity Framework Core rastrea los cambios automáticamente
        }
    }
}
