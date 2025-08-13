using Campus.Conexion;
using Campus.Models;

namespace Campus.Repositories
{
    public class ImplPerfilRepository : IPerfilRepository
    {
        private readonly CampusDbContext campusDbContext;
        public ImplPerfilRepository(CampusDbContext campusDbContext)
        {
            this.campusDbContext = campusDbContext;
        }
        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return campusDbContext.Estudiantes.ToList();
        }
        public void CrearEstudiante(Estudiante estudiante)
        {
            if(estudiante == null)
                throw new ArgumentNullException(nameof(estudiante));
            else
                campusDbContext.Estudiantes.Add(estudiante);
        }
        public bool ExisteEstudiante(int id)
        {
            return campusDbContext.Estudiantes.Any(e => e.id == id);
        }
        
        public bool Guardar()
        {
            return (campusDbContext.SaveChanges() >= 0);
        }

        public bool ExisteEstudianteForaneo(int fci)
        {
            return campusDbContext.Estudiantes.Any(e => e.fci == fci);
        }
    }
}
